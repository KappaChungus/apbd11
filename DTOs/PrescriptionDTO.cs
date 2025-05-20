using System.Collections.ObjectModel;
using Tutorial5.Models;

namespace Tutorial5.DTOs;

public class PrescriptionDTO
{
    public int IdPrescription { get; set; }
    public DateTime DatePrescription { get; set; }
    public DateTime DueDate { get; set; }
    public int IdDoctor { get; set; }
}