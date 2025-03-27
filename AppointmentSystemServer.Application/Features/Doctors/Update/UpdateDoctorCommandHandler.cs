using AppointmentSystemServer.Application.Features.Doctors._Constants;
using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Infrastructure.Caching;
using AutoMapper;
using GenericRepository;
using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Doctors.Update;

public class UpdateDoctorCommandHandler(
    IDoctorRepository doctorRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ICacheService cacheService) : IRequestHandler<UpdateDoctorCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
    {
        Doctor? doctor = await doctorRepository.GetByExpressionAsync(d => d.Id == request.Id, cancellationToken);
        if (doctor is null) return Result<string>.Failure(DoctorConstants.NotFound);

        mapper.Map(request, doctor); // request içerisindeki değerleri doctor classına aktarılacak
        doctorRepository.Update(doctor);
        await unitOfWork.SaveChangesAsync();

        //await cacheService.RemoveAsync(DoctorConstants.CacheKey);
        await cacheService.RemoveByPrefixAsync(new() { DoctorConstants.CacheKey, DoctorConstants.CacheKeyWrite(request.DepartmentId) });
        return DoctorConstants.UpdateMessage;
    }
}