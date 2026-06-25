using Backend_API.models;
using Backend_API.repositories;

namespace Backend_API.services
{
    public class PatientService
    {
        private readonly PatientRepository _repo;

        public PatientService(PatientRepository repo)
        {
            _repo = repo;
        }

        public void AddPatient(patient p)
        {
            _repo.AddPatient(p);
        }
    }
}