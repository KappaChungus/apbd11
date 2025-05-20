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
    
    // public async Task<List<BookWithAuthorsDto>> GetBooks()
    // {
    //     var books = await _context.Books.Select(e =>
    //     new BookWithAuthorsDto {
    //         Name = e.Name,
    //         Price = e.Price,
    //         Authors = e.BookAuthors.Select(a =>
    //         new AuthorDto {
    //             FirstName = a.Author.FirstName,
    //             LastName = a.Author.LastName
    //         }).ToList()
    //     }).ToListAsync();
    //     return books;
    // }

    public async Task<PatientDataDTO> GetPatientData(int patientId)
    {
        var patientData = await _context.Patients
            .Where(p => p.IdPatient == patientId)  // Filtrowanie po konkretnym pacjencie
            .Include(p => p.Prescriptions)          // Ładowanie powiązanych recept
            .Select(p => new PatientDataDTO
            {
                Patient = new PatientDTO
                {
                    IdPatient = p.IdPatient,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    BirthDate = p.BirthDate
                },
                // Przekształcanie listy recept na DTO
                Prescriptions = p.Prescriptions.Select(pr => new PrescriptionDTO
                {
                    IdPrescription = pr.IdPrescription,
                    DatePrescription = pr.DatePrescription,
                    DueDate = pr.DueDate,
                    IdDoctor = pr.IdDoctor
                }).ToList() // Upewnij się, że to jest lista
            })
            .FirstOrDefaultAsync();  // Pierwszy (jedyny) pacjent o tym ID

        return patientData;
    }
    
}