using Backend_API.models;
using Backend_API.repositories;

namespace Backend_API.services
{
    public class AppointmentService
    {
        private readonly AppointmentRepository _repo;

        public AppointmentService(
            AppointmentRepository repo
        )
        {
            _repo = repo;
        }

        public void AddAppointment(Appointment a)
        {
            _repo.AddAppointment(a);
        }
    }
}