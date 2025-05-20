using System.Collections.ObjectModel;

namespace Tutorial5.DTOs;

public class PatientDataDTO
{
    public PatientDTO Patient { get; set; }
    public List<PrescriptionDTO> Prescriptions { get; set; }
    
}