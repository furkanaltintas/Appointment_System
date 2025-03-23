using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Infrastructure.Caching;
using AutoMapper;
using GenericRepository;
using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Departments.Delete;

public class DeleteDepartmentCommandHandler(IDepartmentRepository departmentRepository, IUnitOfWork uow, IMapper mapper, ICacheService cacheService) : IRequestHandler<DeleteDepartmentCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        Department? department = await departmentRepository.GetByExpressionAsync(d => d.Id == request.Id);
        if (department is null) return Result<Unit>.Failure(DepartmentConstants.NotFound);

        departmentRepository.Delete(department);
        await uow.SaveChangesAsync();
        await cacheService.RemoveAsync(DepartmentConstants.CacheKey);
        return Result<Unit>.Succeed(Unit.Value);
    }
}