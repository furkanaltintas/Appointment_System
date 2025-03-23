using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Infrastructure.Caching;
using AutoMapper;
using GenericRepository;
using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Departments.Create;

public class CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository, IUnitOfWork uow, IMapper mapper, ICacheService cacheService) : IRequestHandler<CreateDepartmentCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        Department department = mapper.Map<Department>(request);
        await departmentRepository.AddAsync(department, cancellationToken);
        await uow.SaveChangesAsync(cancellationToken);
        await cacheService.RemoveAsync(DepartmentConstants.CacheKey);
        return Result<Unit>.Succeed(Unit.Value);
    }
}