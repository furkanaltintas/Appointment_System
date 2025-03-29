using AppointmentSystemServer.Application.Features.Patients._Constants;
using AppointmentSystemServer.Application.Features.Patients.Update;
using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Patients._Rules;

public class PatientBusinessRules(IPatientRepository patientRepository)
{
    public Result<string> NotFoundAndIdentityNumberAlreadyUse(Patient patient, UpdatePatientCommand request)
    {
        if (patient is null) return Result<string>.Failure(PatientConstants.NotFound);

        if (patient.IdentityNumber != request.IdentityNumber)
        {
            Boolean existPatient = patientRepository.Any(p => p.IdentityNumber == request.IdentityNumber);
            if (existPatient) return Result<string>.Failure(PatientConstants.IdentityNumberAlreadyUse);
        }

        return String.Empty;
    }
}
