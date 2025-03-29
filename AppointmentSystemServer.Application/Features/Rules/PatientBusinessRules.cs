using AppointmentSystemServer.Application.Features.Commands.PatientCommands;
using AppointmentSystemServer.Application.Features.Constants.PatientConstants;
using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Rules;

public class PatientBusinessRules(IPatientRepository patientRepository)
{
    public Result<string> NotFoundAndIdentityNumberAlreadyUse(Patient patient, UpdatePatientCommand request)
    {
        if (patient is null) return Result<string>.Failure(PatientConstants.NotFound);

        if (patient.IdentityNumber != request.IdentityNumber)
        {
            bool existPatient = patientRepository.Any(p => p.IdentityNumber == request.IdentityNumber);
            if (existPatient) return Result<string>.Failure(PatientConstants.IdentityNumberAlreadyUse);
        }

        return string.Empty;
    }
}
