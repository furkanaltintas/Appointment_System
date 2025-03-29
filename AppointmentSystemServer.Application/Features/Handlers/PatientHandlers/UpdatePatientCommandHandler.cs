using AppointmentSystemServer.Application.Features.Commands.PatientCommands;
using AppointmentSystemServer.Application.Features.Constants.PatientConstants;
using AppointmentSystemServer.Application.Features.Rules;
using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Infrastructure.Caching;
using AutoMapper;
using GenericRepository;
using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Handlers.PatientHandlers;

public class UpdatePatientCommandHandler(
    IPatientRepository patientRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ICacheService cacheService,
    PatientBusinessRules patientBusinessRules) : IRequestHandler<UpdatePatientCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        Patient? patient = await patientRepository.GetByExpressionAsync(d => d.Id == request.Id, cancellationToken);

        var validate = patientBusinessRules.NotFoundAndIdentityNumberAlreadyUse(patient, request);
        return validate.IsSuccessful ? await ContinueProcess(request, patient) : validate;
    }


    private async Task<Result<string>> ContinueProcess(UpdatePatientCommand request, Patient patient)
    {
        mapper.Map(request, patient);
        patientRepository.Update(patient);
        await unitOfWork.SaveChangesAsync();

        await cacheService.RemoveByPrefixAsync(new() { PatientConstants.CacheKey, PatientConstants.CacheKeyGetByIdentityNumber(request.IdentityNumber) });
        return PatientConstants.UpdateMessage;
    }
}