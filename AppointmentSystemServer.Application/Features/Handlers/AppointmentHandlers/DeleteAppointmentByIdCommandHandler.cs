using AppointmentSystemServer.Application.Features.Commands.AppointmentCommands;
using AppointmentSystemServer.Application.Features.Constants.AppointmentConstants;
using AppointmentSystemServer.Application.Features.Rules;
using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Infrastructure.Caching;
using GenericRepository;
using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Handlers.AppointmentHandlers;

public class DeleteAppointmentByIdCommandHandler(
    IAppointmentRepository appointmentRepository,
    IUnitOfWork unitOfWork,
    ICacheService cacheService,
    AppointmentBusinessRules appointmentBusinessRules) : IRequestHandler<DeleteAppointmentByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteAppointmentByIdCommand request, CancellationToken cancellationToken)
    {
        Appointment? appointment = await appointmentRepository.GetByExpressionAsync(a => a.Id == request.Id, cancellationToken);
        var validationResult = appointmentBusinessRules.ValidateAppointmentForDeletion(appointment);
        if (!validationResult.IsSuccessful) return validationResult;

        appointmentRepository.Delete(appointment);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await cacheService.RemoveAsync(AppointmentConstants.CacheKeyGeyByDoctorId(request.DoctorId));

        return AppointmentConstants.DeleteMessage;
    }
}