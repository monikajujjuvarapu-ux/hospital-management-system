namespace Backend_API.models
{
    public class MedicalRecord
    {
        public int RecordId { get; set; }

        public int PatientId { get; set; }

        public string Diagnosis { get; set; } = "";

        public string Description { get; set; } = "";

        public string Prescription { get; set; } = "";

        public DateTime VisitDate { get; set; }
    }
}