using AppointmentSystemServer.Application.Features.Departments._Constants;
using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Infrastructure.Caching;
using AutoMapper;
using GenericRepository;
using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Departments.DeleteById;

public class DeleteDepartmentCommandHandler(IDepartmentRepository departmentRepository, IUnitOfWork uow, IMapper mapper, ICacheService cacheService) : IRequestHandler<DeleteDepartmentCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        Department? department = await departmentRepository.GetByExpressionAsync(d => d.Id == request.Id);
        if (department is null) return Result<string>.Failure(DepartmentConstants.NotFound);

        departmentRepository.Delete(department);
        await uow.SaveChangesAsync();

        await cacheService.RemoveAsync(DepartmentConstants.CacheKey);
        return DepartmentConstants.DeleteMessage;
    }
}