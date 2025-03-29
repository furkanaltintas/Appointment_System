using AppointmentSystemServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Queries.DoctorQueries;

public record GetAllDoctorByDepartmentQuery(
    int DepartmentId) : IRequest<Result<List<Doctor>>>;