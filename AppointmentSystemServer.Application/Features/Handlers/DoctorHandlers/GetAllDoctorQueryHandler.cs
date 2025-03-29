using AppointmentSystemServer.Application.Features.Constants.DoctorConstants;
using AppointmentSystemServer.Application.Features.Queries.DoctorQueries;
using AppointmentSystemServer.Application.Features.Responses.DoctorResponses;
using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Infrastructure.Caching;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Handlers.DoctorHandlers;

public class GetAllDoctorQueryHandler(IDoctorRepository doctorRepository, IMapper mapper, ICacheService cacheService) : IRequestHandler<GetAllDoctorQuery, Result<List<GetAllDoctorResponse>>>
{
    public async Task<Result<List<GetAllDoctorResponse>>> Handle(GetAllDoctorQuery request, CancellationToken cancellationToken)
    {
        var getAllDoctorResponses = await cacheService.GetOrSetAsync(DoctorConstants.CacheKey, async () =>
        {
            List<Doctor> doctors = await doctorRepository.GetAll().Include(d => d.Department).ToListAsync();
            return mapper.Map<List<GetAllDoctorResponse>>(doctors);
        });

        return Result<List<GetAllDoctorResponse>>.Succeed(getAllDoctorResponses);
    }
}
