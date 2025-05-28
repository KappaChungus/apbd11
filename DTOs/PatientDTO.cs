using System.Collections.ObjectModel;
using Tutorial5.Models;

namespace Tutorial5.DTOs;

public class PatientDTO
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    
}