using ApbdApp.DTOs;

namespace ApbdApp.Services;

public interface IPrescriptionService
{
    Task AddPrescriptionAsync(AddPrescriptionRequest request);
}