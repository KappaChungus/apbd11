using Microsoft.EntityFrameworkCore;
using Tutorial5.Data;
using Tutorial5.DTOs;
using Tutorial5.Models;

namespace Tutorial5.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;
    public DbService(DatabaseContext context)
    {
        _context = context;
    }
  

    public async Task<PatientDataDTO> GetPatientData(int patientId)
    {
        var patientData = await _context.Patients
            .Where(p => p.IdPatient == patientId)
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament)
            .Select(p => new PatientDataDTO
            {
                Patient = new PatientDTO
                {
                    IdPatient = p.IdPatient,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    BirthDate = p.Birthdate
                },
                Prescriptions = p.Prescriptions
                    .OrderBy(pr => pr.DueDate)
                    .Select(pr => new PrescriptionDTO
                    {
                        IdPrescription = pr.IdPrescription,
                        DatePrescription = pr.Date,
                        DueDate = pr.DueDate,
                        Medicaments = pr.PrescriptionMedicaments.Select(pm => new MedicamentDTO
                        {
                            Id = pm.Medicament.IdMedicament,
                            Name = pm.Medicament.Name,
                            Dose = pm.Dose,
                            Description = pm.Medicament.Description
                        }).ToList()
                    }).ToList()
            })
            .FirstOrDefaultAsync();


        return patientData;
    }

    public async Task<bool> AddPrescription(NewPrescriptionDTO prescription)
{
    if (prescription.DueDate < prescription.Date)
        throw new ArgumentException("DueDate must be greater than or equal to Date");

    if (prescription.Medicament.Count > 10)
        throw new ArgumentException("Prescription cannot contain more than 10 medicaments");

    var patient = await _context.Patients
        .FirstOrDefaultAsync(p => p.FirstName == prescription.Patient.FirstName &&
                                  p.LastName == prescription.Patient.LastName &&
                                  p.Birthdate == prescription.Patient.BirthDate);

    if (patient == null)
    {
        patient = new Patient
        {
            FirstName = prescription.Patient.FirstName,
            LastName = prescription.Patient.LastName,
            Birthdate = prescription.Patient.BirthDate
        };

        _context.Patients.Add(patient);
        await _context.SaveChangesAsync(); // Save to get IdPatient
    }

    // Check all medicaments exist
    var medicamentIds = prescription.Medicament.Select(m => m.Id).ToList();
    var existingMedicamentIds = await _context.Medicaments
        .Where(m => medicamentIds.Contains(m.IdMedicament))
        .Select(m => m.IdMedicament)
        .ToListAsync();

    if (existingMedicamentIds.Count != medicamentIds.Count)
        throw new ArgumentException("One or more medicaments do not exist");

    // Create prescription
    var newPrescription = new Prescription
    {
        Date = prescription.Date,
        DueDate = prescription.DueDate,
        IdPatient = patient.IdPatient,
    };

    _context.Prescriptions.Add(newPrescription);
    await _context.SaveChangesAsync();

    foreach (var med in prescription.Medicament)
    {
        _context.PrescriptionsMedicaments.Add(new PrescriptionMedicament
        {
            IdPrescription = newPrescription.IdPrescription,
            IdMedicament = med.Id,
            Dose = med.Dose,
            Details = med.Description
        });
    }

    await _context.SaveChangesAsync();

    return true;
}

}