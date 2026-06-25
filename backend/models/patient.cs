namespace Backend_API.models
{
    public class patient
    {
        public int PatientId { get; set; }

        public string Name { get; set; } = "";

        public int Age { get; set; }

        public string Gender { get; set; } = "";

        public string Phone { get; set; } = "";

        public string Address { get; set; } = "";

        public string Status { get; set; } = "";
    }
}