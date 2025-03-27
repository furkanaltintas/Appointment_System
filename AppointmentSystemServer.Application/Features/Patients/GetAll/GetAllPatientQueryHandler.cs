using AppointmentSystemServer.Application.Features.Patients._Constants;
using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Infrastructure.Caching;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Patients.GetAll;

public class GetAllPatientQueryHandler(IPatientRepository patientRepository, ICacheService cacheService) : IRequestHandler<GetAllPatientQuery, Result<List<Patient>>>
{
    public async Task<Result<List<Patient>>> Handle(GetAllPatientQuery request, CancellationToken cancellationToken)
    {
        var getAllPatientResponses = await cacheService.GetOrSetAsync(PatientConstants.CacheKey, async () =>
        {
            List<Patient> patients = await patientRepository.GetAll().OrderBy(p => p.FirstName).ThenBy(p => p.LastName).ToListAsync();
            return patients;
        });

        return Result<List<Patient>>.Succeed(getAllPatientResponses);
    }
}