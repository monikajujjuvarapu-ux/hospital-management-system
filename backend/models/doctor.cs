namespace Backend_API.models
{
    public class doctor
    {
        public int DoctorId { get; set; }

        public string DoctorName { get; set; } = "";

        public string Specialization { get; set; } = "";

        public string Phone { get; set; } = "";

        public string Email { get; set; } = "";
    }
}