namespace Tutorial5.DTOs;

public class NewPrescriptionDTO
{
    public PatientDTO Patient { get; set; }
    public List<MedicamentDTO> Medicament { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    
}