using AppointmentSystemServer.Application.Features.Constants.DoctorConstants;
using AppointmentSystemServer.Application.Features.Queries.DoctorQueries;
using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Infrastructure.Caching;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Handlers.DoctorHandlers;

class GetAllDoctorByDepartmentQueryHandler(IDoctorRepository doctorRepository, ICacheService cacheService) : IRequestHandler<GetAllDoctorByDepartmentQuery, Result<List<Doctor>>>
{
    public async Task<Result<List<Doctor>>> Handle(GetAllDoctorByDepartmentQuery request, CancellationToken cancellationToken)
    {
        var doctors = await cacheService.GetOrSetAsync(DoctorConstants.CacheKeyDepartmentById(request.DepartmentId), async () =>
        {
            return await doctorRepository
            .Where(d => d.DepartmentId == request.DepartmentId)
            .OrderBy(d => d.Department.Name)
            .ToListAsync();
        });

        return Result<List<Doctor>>.Succeed(doctors);
    }
}