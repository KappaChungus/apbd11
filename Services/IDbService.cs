using Tutorial5.DTOs;

namespace Tutorial5.Services;

public interface IDbService
{
    Task<PatientDataDTO> GetPatientData(int patientId);
}