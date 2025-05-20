using Microsoft.EntityFrameworkCore;
using Tutorial5.Models;

namespace Tutorial5.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionsMedicaments { get; set; }
    
    
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>().HasData(new List<Doctor>
        {
            new Doctor
            {
                IdDoctor = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            },
            new Doctor
            {
                IdDoctor = 2,
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com"
            },
            new Doctor
            {
                IdDoctor = 3,
                FirstName = "Emily",
                LastName = "Clark",
                Email = "emily.clark@example.com"
            }
        });
        
        modelBuilder.Entity<Medicament>().HasData(new List<Medicament>
        {
            new Medicament
            {
                IdMedicament = 1,
                Name = "Paracetamol",
                Description = "Pain reliever and fever reducer",
                Type = "Tablet"
            },
            new Medicament
            {
                IdMedicament = 2,
                Name = "Ibuprofen",
                Description = "Anti-inflammatory and pain reliever",
                Type = "Capsule"
            },
            new Medicament
            {
                IdMedicament = 3,
                Name = "Amoxicillin",
                Description = "Antibiotic used for bacterial infections",
                Type = "Suspension"
            }
        });
        
        modelBuilder.Entity<Patient>().HasData(new List<Patient>
        {
            new Patient
            {
                IdPatient = 1,
                FirstName = "Alice",
                LastName = "Brown",
                BirthDate = new DateTime(1990, 5, 15)
            },
            new Patient
            {
                IdPatient = 2,
                FirstName = "Bob",
                LastName = "Johnson",
                BirthDate = new DateTime(1985, 10, 23)
            },
            new Patient
            {
                IdPatient = 3,
                FirstName = "Charlie",
                LastName = "Davis",
                BirthDate = new DateTime(2000, 1, 8)
            }
        });
        
        modelBuilder.Entity<Prescription>().HasData(new List<Prescription>
        {
            new Prescription
            {
                IdPrescription = 1,
                DatePrescription = new DateTime(2025, 5, 1),
                DueDate = new DateTime(2025, 6, 1),
                IdPatient = 1, // Referencing patient with Id 1
                IdDoctor = 1  // Referencing doctor with Id 1
            },
            new Prescription
            {
                IdPrescription = 2,
                DatePrescription = new DateTime(2025, 5, 2),
                DueDate = new DateTime(2025, 6, 2),
                IdPatient = 2, // Referencing patient with Id 2
                IdDoctor = 2  // Referencing doctor with Id 2
            },
            new Prescription
            {
                IdPrescription = 3,
                DatePrescription = new DateTime(2025, 5, 3),
                DueDate = new DateTime(2025, 6, 3),
                IdPatient = 3, // Referencing patient with Id 3
                IdDoctor = 3  // Referencing doctor with Id 3
            }
        });

        modelBuilder.Entity<PrescriptionMedicament>().HasData(new List<PrescriptionMedicament>
        {
            new PrescriptionMedicament
            {
                IdPrescription = 1,   // Referencing Prescription with Id 1
                IdMedicament = 1,     // Referencing Medicament with Id 1 (Paracetamol)
                Dose = 500,           // Dose in mg
                Details = "Take 1 tablet every 6 hours"
            },
            new PrescriptionMedicament
            {
                IdPrescription = 1,   // Referencing Prescription with Id 1
                IdMedicament = 2,     // Referencing Medicament with Id 2 (Ibuprofen)
                Dose = 200,           // Dose in mg
                Details = "Take 1 capsule every 8 hours"
            },
            new PrescriptionMedicament
            {
                IdPrescription = 2,   // Referencing Prescription with Id 2
                IdMedicament = 3,     // Referencing Medicament with Id 3 (Amoxicillin)
                Dose = 250,           // Dose in mg
                Details = "Take 1 suspension 3 times a day"
            }
        });
        
    }
}