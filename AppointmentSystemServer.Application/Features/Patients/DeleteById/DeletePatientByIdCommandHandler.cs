using AppointmentSystemServer.Application.Features.Patients._Constants;
using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Infrastructure.Caching;
using GenericRepository;
using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Patients.DeleteById;

public class DeletePatientByIdCommandHandler(
    IPatientRepository patientRepository,
    IUnitOfWork unitOfWork,
    ICacheService cacheService) : IRequestHandler<DeletePatientByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeletePatientByIdCommand request, CancellationToken cancellationToken)
    {
        Patient? patient = await patientRepository.GetByExpressionAsync(d => d.Id == int.Parse(request.Id));
        if (patient == null) return Result<string>.Failure(PatientConstants.NoSuch);
        patientRepository.Delete(patient);
        await unitOfWork.SaveChangesAsync();

        await cacheService.RemoveByPrefixAsync(new() { PatientConstants.CacheKey, PatientConstants.CacheKeyGetByIdentityNumber(patient.IdentityNumber) });
        return PatientConstants.DeleteMessage;
    }
}