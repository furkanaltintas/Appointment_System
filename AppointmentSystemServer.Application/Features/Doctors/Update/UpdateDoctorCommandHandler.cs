using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Infrastructure.Caching;
using AutoMapper;
using GenericRepository;
using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Doctors.Update;

public class UpdateDoctorCommandHandler(IDoctorRepository doctorRepository, IUnitOfWork uow,IMapper mapper, ICacheService cacheService) : IRequestHandler<UpdateDoctorCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
    {
        Doctor? doctor = await doctorRepository.GetByExpressionAsync(d => d.Id == request.Id, cancellationToken);
        if (doctor is null) return Result<Unit>.Failure(DoctorConstants.NotFound);

        mapper.Map(request, doctor);

        doctorRepository.Update(doctor);
        await uow.SaveChangesAsync();

        await cacheService.RemoveAsync(DoctorConstants.CacheKey);
        return Result<Unit>.Succeed(Unit.Value);
    }
}