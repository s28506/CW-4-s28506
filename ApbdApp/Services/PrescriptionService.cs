using ApbdApp.DAL;
using ApbdApp.DTOs;
using ApbdApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ApbdApp.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly IMyDbContext _context;

    public PrescriptionService(IMyDbContext context)
    {
        _context = context;
    }

    public async Task AddPrescriptionAsync(AddPrescriptionRequest request)
    {
        if (request.Medicaments.Count > 10)
            throw new ArgumentException("Prescription cannot contain more than 10 medicaments.");

        if (request.DueDate < request.Date)
            throw new ArgumentException("DueDate must be later or equal to Date.");
        
        var doctorExists = await _context.Doctors.AnyAsync(d => d.IdDoctor == request.IdDoctor);
        if (!doctorExists)
        {
            throw new ArgumentException($"Doctor with ID {request.IdDoctor} does not exist.");
        }


        var patient = await _context.Patients
            .FirstOrDefaultAsync(p =>
                p.FirstName == request.Patient.FirstName &&
                p.LastName == request.Patient.LastName &&
                p.BirthDate == request.Patient.BirthDate);

        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = request.Patient.FirstName,
                LastName = request.Patient.LastName,
                BirthDate = request.Patient.BirthDate
            };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        foreach (var m in request.Medicaments)
        {
            var exists = await _context.Medicaments.AnyAsync(x => x.IdMedicament == m.IdMedicament);
            if (!exists)
                throw new ArgumentException($"Medicament with ID {m.IdMedicament} does not exist.");
        }

        var prescription = new Prescription
        {
            Date = request.Date,
            DueDate = request.DueDate,
            IdDoctor = request.IdDoctor,
            IdPatient = patient.IdPatient,
            PrescriptionMedicaments = request.Medicaments.Select(m => new PrescriptionMedicament
            {
                IdMedicament = m.IdMedicament,
                Dose = m.Dose,
                Details = m.Details
            }).ToList()
        };

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();
    }
}
