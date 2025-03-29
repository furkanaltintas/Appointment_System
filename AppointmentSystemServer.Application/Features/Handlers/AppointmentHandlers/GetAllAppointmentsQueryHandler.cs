using AppointmentSystemServer.Application.Features.Constants.AppointmentConstants;
using AppointmentSystemServer.Application.Features.Queries.AppointmentQueries;
using AppointmentSystemServer.Application.Features.Responses.AppointmentResponses;
using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Infrastructure.Caching;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Handlers.AppointmentHandlers;

public class GetAllAppointmentsQueryHandler(
    IAppointmentRepository appointmentRepository,
    IMapper mapper,
    ICacheService cacheService) : IRequestHandler<GetAllAppointmentsQuery, Result<List<GetAllAppointmentsQueryResponse>>>
{
    public async Task<Result<List<GetAllAppointmentsQueryResponse>>> Handle(GetAllAppointmentsQuery request, CancellationToken cancellationToken)
    {
        List<Appointment> appointments = await appointmentRepository
            .Where(a => a.DoctorId == request.DoctorId)
            .Include(a => a.Patient)
            .ToListAsync(cancellationToken);

        List<GetAllAppointmentsQueryResponse> getAllAppointmentsQueryResponses = await cacheService.GetOrSetAsync(
            AppointmentConstants.CacheKeyGeyByDoctorId(request.DoctorId),
            async () =>
            {
                return mapper.Map<List<GetAllAppointmentsQueryResponse>>(appointments);
            });

        return Result<List<GetAllAppointmentsQueryResponse>>.Succeed(getAllAppointmentsQueryResponses);
    }
}