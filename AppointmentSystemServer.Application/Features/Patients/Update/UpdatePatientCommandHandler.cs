using AppointmentSystemServer.Application.Features.Patients._Constants;
using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Infrastructure.Caching;
using AutoMapper;
using GenericRepository;
using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Patients.Update;

public class UpdatePatientCommandHandler(
    IPatientRepository patientRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ICacheService cacheService) : IRequestHandler<UpdatePatientCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        Patient? patient = await patientRepository.GetByExpressionAsync(d => d.Id == request.Id, cancellationToken);
        if (patient is null) return Result<string>.Failure(PatientConstants.NotFound);

        if (patient.IdentityNumber != request.IdentityNumber)
        {
            Boolean existPatient = patientRepository.Any(p => p.IdentityNumber == request.IdentityNumber);
            if (existPatient) return Result<string>.Failure(PatientConstants.IdentityNumberAlreadyUse);
        }

        mapper.Map(request, patient);
        patientRepository.Update(patient);
        await unitOfWork.SaveChangesAsync();

        await cacheService.RemoveByPrefixAsync(new() { PatientConstants.CacheKey, PatientConstants.CacheKeyGetByIdentityNumber(request.IdentityNumber) });
        return PatientConstants.UpdateMessage;
    }
}