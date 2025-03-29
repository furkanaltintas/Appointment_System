using AppointmentSystemServer.Application.Features.Commands.DoctorCommands;
using AppointmentSystemServer.Application.Features.Constants.DoctorConstants;
using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Infrastructure.Caching;
using AutoMapper;
using GenericRepository;
using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Handlers.DoctorHandlers;

public class CreateDoctorCommandHandler(
    IDoctorRepository doctorRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ICacheService cacheService) : IRequestHandler<CreateDoctorCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
    {
        Doctor doctor = mapper.Map<Doctor>(request);

        await doctorRepository.AddAsync(doctor, cancellationToken);
        await unitOfWork.SaveChangesAsync();

        await cacheService.RemoveByPrefixAsync(new() { DoctorConstants.CacheKey, DoctorConstants.CacheKeyWrite(request.DepartmentId) });
        return DoctorConstants.CreateMessage;
    }
}
