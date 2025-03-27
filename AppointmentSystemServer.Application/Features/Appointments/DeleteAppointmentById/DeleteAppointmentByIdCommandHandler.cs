using AppointmentSystemServer.Application.Features.Appointments._Constants;
using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Infrastructure.Caching;
using GenericRepository;
using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Appointments.DeleteAppointmentById
{
    public class DeleteAppointmentByIdCommandHandler(
        IAppointmentRepository appointmentRepository,
        IUnitOfWork unitOfWork,
        ICacheService cacheService) : IRequestHandler<DeleteAppointmentByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteAppointmentByIdCommand request, CancellationToken cancellationToken)
        {
            Appointment? appointment = await appointmentRepository.GetByExpressionAsync(a => a.Id == request.Id, cancellationToken);
            if (appointment == null) return Result<string>.Failure(AppointmentConstants.NotFound);

            if (appointment.IsCompleted) return Result<string>.Failure(AppointmentConstants.YouCannotDeleteACompleted);

            appointmentRepository.Delete(appointment);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            await cacheService.RemoveAsync(AppointmentConstants.CacheKeyGeyByDoctorId(request.DoctorId));
            return AppointmentConstants.DeleteMessage;
        }
    }
}
