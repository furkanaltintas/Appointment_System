using AppointmentSystemServer.Application.Features.Departments._Constants;
using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Infrastructure.Caching;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Departments.GetAll;

public class GetAllDepartmentQueryHandler(
    IDepartmentRepository departmentRepository,
    IMapper mapper,
    ICacheService cacheService) : IRequestHandler<GetAllDepartmentQuery, Result<List<GetAllDepartmentResponse>>>
{
    public async Task<Result<List<GetAllDepartmentResponse>>> Handle(GetAllDepartmentQuery request, CancellationToken cancellationToken)
    {
        var getAllDepartmentResponses = await cacheService.GetOrSetAsync(DepartmentConstants.CacheKey, async () =>
        {
            List<Department> departments = await departmentRepository.GetAll().OrderBy(d => d.Name).ToListAsync();
            return mapper.Map<List<GetAllDepartmentResponse>>(departments);
        });

        return Result<List<GetAllDepartmentResponse>>.Succeed(getAllDepartmentResponses);
    }
}