using ApbdApp.Models;

namespace ApbdApp.DAL;

using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public interface IMyDbContext
{
    DbSet<Doctor> Doctors { get; }
    DbSet<Medicament> Medicaments { get; }
    DbSet<Patient> Patients { get; }
    DbSet<Prescription> Prescriptions { get; }
    DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
