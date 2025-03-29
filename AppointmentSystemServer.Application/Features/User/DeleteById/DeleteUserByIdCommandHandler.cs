using AppointmentSystemServer.Application.Commons;
using AppointmentSystemServer.Application.Features.User._Rules;
using AppointmentSystemServer.Application.Features.Users._Constants;
using AppointmentSystemServer.Infrastructure.Caching;
using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.User.DeleteById;

class DeleteUserByIdCommandHandler(
    UserBusinessRules userBusinessRules,
    ICacheService cacheService) : IRequestHandler<DeleteUserByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
    {
        Result<string> result = ResultValidate.Run(
             await userBusinessRules.NotFoundAsync(request.Id),
             await userBusinessRules.NotDeleteAsync(request.Id));

        await cacheService.RemoveAsync(UserConstants.CacheKey);
        return result.IsSuccessful ? UserConstants.DeleteMessage : result;
    }
}