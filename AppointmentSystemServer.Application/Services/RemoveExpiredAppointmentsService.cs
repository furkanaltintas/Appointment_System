using AppointmentSystemServer.Application.Features.Appointments._Constants;
using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Infrastructure.Caching;
using AppointmentSystemServer.Infrastructure.SignalR.Hubs;
using GenericRepository;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class RemoveExpiredAppointmentsService(
    IServiceScopeFactory serviceScopeFactory,
    IHubContext<AppointmentHub> hubContext,
    ICacheService cacheService) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                // Gelecek randevuları al
                var futureAppointments = await GetFutureAppointmentsAsync(stoppingToken);

                // Her bir randevu için bekleme süresi ayarla
                foreach (var appointment in futureAppointments)
                {
                    var delay = appointment.EndDate - DateTime.Now; // Randevuya kadar kalan süre
                    if (delay.TotalSeconds > 0)
                    {
                        // Bekleme süresi kadar bekle
                        await Task.Delay(delay, stoppingToken);

                        // Zaman geldiğinde randevuyu sil
                        await RemoveAppointmentAsync(appointment, stoppingToken);
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda loglama yapılabilir
                throw new Exception("Error while removing expired appointments", ex);
            }
        }
    }

    private async Task<IEnumerable<Appointment>> GetFutureAppointmentsAsync(CancellationToken stoppingToken)
    {
        using (var scope = serviceScopeFactory.CreateScope())
        {
            var appointmentRepository = scope.ServiceProvider.GetRequiredService<IAppointmentRepository>();

            // Gelecek randevuları al
            var futureAppointments = appointmentRepository
                .GetAll()
                .Where(a => a.EndDate > DateTime.Now)
                .ToList(); // Listeye dönüştürmek önemli

            return futureAppointments;
        }
    }

    private async Task RemoveAppointmentAsync(Appointment appointment, CancellationToken stoppingToken)
    {
        using (var scope = serviceScopeFactory.CreateScope())
        {
            var appointmentRepository = scope.ServiceProvider.GetRequiredService<IAppointmentRepository>();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            // Randevuyu sil
            appointmentRepository.Delete(appointment);
            await unitOfWork.SaveChangesAsync();
            await cacheService.RemoveAsync(AppointmentConstants.CacheKeyGeyByDoctorId(appointment.DoctorId));
            await hubContext.Clients.All.SendAsync("ReceiveAppointmentDeleted", appointment.Id.ToString(), stoppingToken);
        }
    }
}