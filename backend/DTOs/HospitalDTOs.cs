using System;

public class AppointmentDTO
{
    public int AppointmentId { get; set; }
    public int PatientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public int DoctorId { get; set; }
    public string DoctorName { get; set; } = string.Empty;
    public DateTime AppointmentDate { get; set; }
    public string IssueDescription { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}

public class MedicalRecordDTO
{
    public int RecordId { get; set; }
    public int PatientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public string DiagnosisDescription { get; set; } = string.Empty;
    public string TreatmentPlan { get; set; } = string.Empty;
}