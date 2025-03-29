using AppointmentSystemServer.Application.Features.Responses.UserResponses;
using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Queries.UserQueries;

public record GetAllUserQuery : IRequest<Result<List<GetAllUserQueryResponse>>>;