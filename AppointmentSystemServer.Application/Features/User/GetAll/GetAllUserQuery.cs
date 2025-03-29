using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.User.GetAll;

public record GetAllUserQuery : IRequest<Result<List<GetAllUserQueryResponse>>>;