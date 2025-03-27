using AppointmentSystemServer.Application.Features.Patients._Constants;
using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Infrastructure.Caching;
using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Patients.GetPatientByIdentityNumber;

class GetPatientByIdentityNumberQueryHandler(IPatientRepository patientRepository, ICacheService cacheService) : IRequestHandler<GetPatientByIdentityNumberQuery, Result<Patient>>
{
    public async Task<Result<Patient>> Handle(GetPatientByIdentityNumberQuery request, CancellationToken cancellationToken)
    {
        var patient = await cacheService.GetOrSetAsync(PatientConstants.CacheKeyGetByIdentityNumber(request.IdentityNumber), async () =>
        {
            return await patientRepository.GetByExpressionAsync(p => p.IdentityNumber == request.IdentityNumber, cancellationToken);
        });

        return patient;
    }
}
