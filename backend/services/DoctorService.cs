using Backend_API.models;
using Backend_API.repositories;

namespace Backend_API.services
{
    public class DoctorService
    {
        private readonly DoctorRepository _repo;

        public DoctorService(DoctorRepository repo)
        {
            _repo = repo;
        }

        public void AddDoctor(doctor d)
        {
            _repo.AddDoctor(d);
        }
    }
}