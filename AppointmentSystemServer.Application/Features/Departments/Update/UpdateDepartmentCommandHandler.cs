using AppointmentSystemServer.Application.Features.Departments._Constants;
using AppointmentSystemServer.Application.Features.Doctors._Constants;
using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Infrastructure.Caching;
using AutoMapper;
using GenericRepository;
using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Departments.Update;

public class UpdateDepartmentCommandHandler(
    IDepartmentRepository departmentRepository, 
    IUnitOfWork uow, 
    IMapper mapper, 
    ICacheService cacheService) : IRequestHandler<UpdateDepartmentCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        Department? department = await departmentRepository.GetByExpressionAsync(d => d.Id == request.Id, cancellationToken);
        if (department is null) return Result<string>.Failure(DoctorConstants.NotFound);

        mapper.Map(request, department);
        departmentRepository.Update(department);
        await uow.SaveChangesAsync();

        await cacheService.RemoveAsync(DepartmentConstants.CacheKey);
        return DepartmentConstants.DeleteMessage;
    }
}