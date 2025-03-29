using AppointmentSystemServer.Application.Features.Commands.DoctorCommands;
using AppointmentSystemServer.Application.Features.Constants.DoctorConstants;
using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Infrastructure.Caching;
using GenericRepository;
using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Handlers.DoctorHandlers;

public class DeleteDoctorByIdCommandHandler(
    IDoctorRepository doctorRepository,
    IUnitOfWork unitOfWork,
    ICacheService cacheService) : IRequestHandler<DeleteDoctorByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteDoctorByIdCommand request, CancellationToken cancellationToken)
    {
        Doctor? doctor = await doctorRepository.GetByExpressionAsync(d => d.Id == int.Parse(request.Id));
        if (doctor == null) return Result<string>.Failure(DoctorConstants.NoSuch);
        doctorRepository.Delete(doctor);
        await unitOfWork.SaveChangesAsync();

        //await cacheService.RemoveAsync(DoctorConstants.CacheKey);
        await cacheService.RemoveByPrefixAsync(new() { DoctorConstants.CacheKey, DoctorConstants.CacheKeyWrite(int.Parse(request.Id)) });
        return DoctorConstants.DeleteMessage;
    }
}