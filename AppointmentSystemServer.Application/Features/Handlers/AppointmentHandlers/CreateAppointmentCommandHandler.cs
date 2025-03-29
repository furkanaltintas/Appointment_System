using AppointmentSystemServer.Application.Features.Commands.AppointmentCommands;
using AppointmentSystemServer.Application.Features.Constants.AppointmentConstants;
using AppointmentSystemServer.Application.Features.Constants.PatientConstants;
using AppointmentSystemServer.Application.Features.Rules;
using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Infrastructure.Caching;
using GenericRepository;
using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Handlers.AppointmentHandlers;

class CreateAppointmentCommandHandler(
    IAppointmentRepository appointmentRepository,
    IPatientRepository patientRepository,
    IUnitOfWork unitOfWork,
    ICacheService cacheService,
    AppointmentBusinessRules appointmentBusinessRules) : IRequestHandler<CreateAppointmentCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        bool isPatient = false;
        Patient patient = new();

        Result<string> result = await appointmentBusinessRules.ValidateAppointmentDate(request.DoctorId, DateTime.Parse(request.StartDate), DateTime.Parse(request.EndDate), cancellationToken);
        return result.IsSuccessful ? await ContinueProcess(request, patient, isPatient, cancellationToken) : result;
    }


    private async Task<Result<string>> ContinueProcess(CreateAppointmentCommand request, Patient patient, bool isPatient, CancellationToken cancellationToken)
    {
        if (request.PatientId is 0)
        {
            patient = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                IdentityNumber = request.IdentityNumber,
                City = request.City,
                Town = request.Town,
                FullAddress = request.FullAddress
            };

            patientRepository.Add(patient);
            await unitOfWork.SaveChangesAsync();

            await cacheService.GetOrSetAsync(PatientConstants.CacheKeyGetByIdentityNumber(patient.IdentityNumber), async () => patient);
            isPatient = true;
        }

        Appointment appointment = new()
        {
            DoctorId = request.DoctorId,
            PatientId = request.PatientId != 0 ? request.PatientId : patient.Id,
            StartDate = DateTime.Parse(request.StartDate),
            EndDate = DateTime.Parse(request.EndDate),
            IsCompleted = false
        };

        appointmentRepository.Add(appointment);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        if (isPatient) await cacheService.RemoveAsync(PatientConstants.CacheKey);
        await cacheService.RemoveAsync(AppointmentConstants.CacheKeyGeyByDoctorId(appointment.DoctorId));
        return AppointmentConstants.CreateMessage;
    }
}