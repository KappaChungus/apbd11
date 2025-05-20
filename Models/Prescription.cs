using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tutorial5.Models;

[Table("Prescription")]
public class Prescription
{
    public int IdPrescription { get; set; }
    public DateTime DatePrescription { get; set; }
    public DateTime DueDate { get; set; }
    public int IdPatient { get; set; }
    public int IdDoctor { get; set; }

    public Collection<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
}