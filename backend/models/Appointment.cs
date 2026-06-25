namespace Backend_API.models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string AppointmentTime { get; set; } = "";

        // ✅ FIXED PROPERLY
        public string Status { get; set; } = "Scheduled";
    }
}