using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Infrastructure.Caching;
using GenericRepository;
using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Doctors.Delete;

public class DeleteDoctorCommandHandler(
    IDoctorRepository doctorRepository, 
    IUnitOfWork uow,
    ICacheService cacheService) : IRequestHandler<DeleteDoctorCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
    {
        Doctor? doctor = await doctorRepository.GetByExpressionAsync(d => d.Id == request.Id);
        if (doctor == null) return Result<Unit>.Failure(DoctorConstants.NoSuch);
        doctorRepository.Delete(doctor);
        await uow.SaveChangesAsync();
        await cacheService.RemoveAsync(DoctorConstants.CacheKey);
        return Result<Unit>.Succeed(Unit.Value);
    }
}