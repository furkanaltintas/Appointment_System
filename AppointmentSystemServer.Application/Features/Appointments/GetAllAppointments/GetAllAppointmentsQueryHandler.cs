using AppointmentSystemServer.Application.Dtos;
using AppointmentSystemServer.Application.Features.Appointments._Constants;
using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Infrastructure.Caching;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Appointments.GetAllAppointments;

public class GetAllAppointmentsQueryHandler(IAppointmentRepository appointmentRepository, ICacheService cacheService) : IRequestHandler<GetAllAppointmentsQuery, Result<List<GetAllAppointmentsQueryResponse>>>
{
    public async Task<Result<List<GetAllAppointmentsQueryResponse>>> Handle(GetAllAppointmentsQuery request, CancellationToken cancellationToken)
    {
        List<GetAllAppointmentsQueryResponse> getAllAppointmentsQueryResponses = await cacheService.GetOrSetAsync(
            AppointmentConstants.CacheKeyGeyByDoctorId(request.DoctorId),
            async () =>
            {
                List<Appointment> appointments = await appointmentRepository
                .Where(a => a.DoctorId == request.DoctorId)
                .Include(a => a.Patient)
                .ToListAsync(cancellationToken);

                List<GetAllAppointmentsQueryResponse> getAllAppointmentsQueryResponses = appointments
                .Select(a => new GetAllAppointmentsQueryResponse(
                    a.Id.ToString(),
                    a.StartDate,
                    a.EndDate,
                    a.Patient.FullName,
                    new PatientDto(
                        a.Patient.Id,
                        a.Patient.FirstName,
                        a.Patient.LastName,
                        a.Patient.IdentityNumber,
                        a.Patient.City,
                        a.Patient.Town,
                        a.Patient.FullName))).ToList();
                return getAllAppointmentsQueryResponses;
            });

        return Result<List<GetAllAppointmentsQueryResponse>>.Succeed(getAllAppointmentsQueryResponses);
    }
}