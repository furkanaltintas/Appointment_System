using AppointmentSystemServer.Application.Features.Appointments._Constants;
using AppointmentSystemServer.Application.Services.Repositories;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Appointments._Rules;

public class AppointmentBusinessRules(IAppointmentRepository appointmentRepository)
{
    public async Task<Result<string>> ValidateAppointmentDate(int doctorId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
    {
        DateTime now = DateTime.Now;

        if (endDate < now) return Result<string>.Failure(AppointmentConstants.ValidateAppointmentDate);

        return await IsAppointmentDateNotAvailable(doctorId, startDate, endDate, cancellationToken)
            ? Result<string>.Failure(AppointmentConstants.DateIsNotAvailable)
            : "";
    }

    private async Task<bool> IsAppointmentDateNotAvailable(int doctorId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
    {
        bool isAppointmentDateNotAvailable = await appointmentRepository
            .AnyAsync
            (a => a.DoctorId == doctorId &&
            (a.StartDate < endDate && a.StartDate >= startDate) || // Mevcut randevunun bitişi, diğer randevunun başlangıcıyla çakışıyor mu ?
            (a.EndDate > startDate && a.EndDate < endDate) || // Mevcut randevunun başlangıcı, diğer randevunun bitişiyle çakışıyor mu ?
            (a.StartDate >= startDate && a.EndDate <= endDate) || // Mevcut randevu, diğer randevu içinde mi tamamen
            (a.StartDate <= startDate && a.EndDate >= endDate),
            cancellationToken); // Mevcut randevu, diğer randevuyu tamamen kapsıyor mu

        return isAppointmentDateNotAvailable;
    }
}
