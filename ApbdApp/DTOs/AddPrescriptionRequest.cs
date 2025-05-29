﻿namespace ApbdApp.DTOs;

public class AddPrescriptionRequest
{
    public PatientDto Patient { get; set; }
    public List<PrescriptionMedicamentDto> Medicaments { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public int IdDoctor { get; set; }
}