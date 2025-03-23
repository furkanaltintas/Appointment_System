using AppointmentSystemServer.Application.Features.Doctors.GetAll;
using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Infrastructure.Caching;
using AutoMapper;
using GenericRepository;
using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Doctors.Create;

public class CreateDoctorCommandHandler(
    IDoctorRepository doctorRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ICacheService cacheService) : IRequestHandler<CreateDoctorCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
    {
        Doctor doctor = mapper.Map<Doctor>(request);

        await doctorRepository.AddAsync(doctor, cancellationToken);
        await unitOfWork.SaveChangesAsync();

        await cacheService.RemoveAsync(DoctorConstants.CacheKey);
        return Result<Unit>.Succeed(Unit.Value);
    }
}
