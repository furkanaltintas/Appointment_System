using AppointmentSystemServer.Application.Features.Patients._Constants;
using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Infrastructure.Caching;
using AutoMapper;
using GenericRepository;
using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Patients.Create;

public class CreatePatientCommandHandler(
    IPatientRepository patientRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ICacheService cacheService) : IRequestHandler<CreatePatientCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        Boolean patientExist = patientRepository.Any(p => p.IdentityNumber == request.IdentityNumber);
        if (patientExist) return Result<string>.Failure(PatientConstants.IdentityNumberAlreadyUse);

        Patient patient = mapper.Map<Patient>(request);
        await patientRepository.AddAsync(patient, cancellationToken);
        await unitOfWork.SaveChangesAsync();

        await cacheService.RemoveAsync(PatientConstants.CacheKey);
        return PatientConstants.CreateMessage;
    }
}