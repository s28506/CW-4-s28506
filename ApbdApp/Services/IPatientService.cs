using ApbdApp.DTOs;

namespace ApbdApp.Services;

public interface IPatientService
{
    Task<PatientDetailsDto> GetPatientDetailsAsync(int idPatient);
}
