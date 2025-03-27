using AppointmentSystemServer.Application.Features.Appointments._Constants;
using AppointmentSystemServer.Application.Features.Appointments._Rules;
using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Infrastructure.Caching;
using GenericRepository;
using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Appointments.UpdateAppointment;

public class UpdateAppointmentCommandHandler(
    IAppointmentRepository appointmentRepository,
    IUnitOfWork unitOfWork,
    ICacheService cacheService,
    AppointmentBusinessRules appointmentBusinessRules) : IRequestHandler<UpdateAppointmentCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
    {
        DateTime startDate = DateTime.Parse(request.StartDate);
        DateTime endDate = DateTime.Parse(request.EndDate);

        Appointment? appointment = await appointmentRepository.GetByExpressionWithTrackingAsync(a => a.Id == request.Id, cancellationToken);

        if (appointment == null) return Result<string>.Failure(AppointmentConstants.NotFound);

        Result<string> result = await appointmentBusinessRules.ValidateAppointmentDate(appointment.DoctorId, startDate, endDate, cancellationToken);
        return result.IsSuccessful ? await ContinueProcess(appointment, startDate, endDate, cancellationToken) : result;
    }

    private async Task<Result<string>> ContinueProcess(Appointment appointment, DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
    {
        appointment.StartDate = startDate;
        appointment.EndDate = endDate;
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await cacheService.RemoveAsync(AppointmentConstants.CacheKeyGeyByDoctorId(appointment.DoctorId));
        return AppointmentConstants.UpdateMessage;
    }
}
