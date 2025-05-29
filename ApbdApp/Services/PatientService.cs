using ApbdApp.DAL;
using ApbdApp.DTOs;
using ApbdApp.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ApbdApp.Services;

public class PatientService : IPatientService
{
    private readonly IMyDbContext _context;

    public PatientService(IMyDbContext context)
    {
        _context = context;
    }

    public async Task<PatientDetailsDto> GetPatientDetailsAsync(int idPatient)
    {
        var patient = await _context.Patients
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.Doctor)
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament)
            .FirstOrDefaultAsync(p => p.IdPatient == idPatient);

        if (patient == null)
            throw new PatientNotFoundException();

        return new PatientDetailsDto
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            BirthDate = patient.BirthDate,
            Prescriptions = patient.Prescriptions
                .OrderBy(pr => pr.DueDate)
                .Select(pr => new PrescriptionDetailsDto
                {
                    IdPrescription = pr.IdPrescription,
                    Date = pr.Date,
                    DueDate = pr.DueDate,
                    Doctor = new DoctorDto
                    {
                        IdDoctor = pr.Doctor.IdDoctor,
                        FirstName = pr.Doctor.FirstName,
                        LastName = pr.Doctor.LastName,
                        Email = pr.Doctor.Email
                    },
                    Medicaments = pr.PrescriptionMedicaments.Select(pm => new MedicamentDetailsDto
                    {
                        IdMedicament = pm.Medicament.IdMedicament,
                        Name = pm.Medicament.Name,
                        Dose = pm.Dose ?? 0,
                        Description = pm.Details
                    }).ToList()
                }).ToList()
        };
    }
}