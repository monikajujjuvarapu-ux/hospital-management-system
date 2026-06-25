using Backend_API.models;
using Backend_API.repositories;

namespace Backend_API.services
{
    public class MedicalRecordService
    {
        private readonly MedicalRecordRepository _repo;

        public MedicalRecordService(MedicalRecordRepository repo)
        {
            _repo = repo;
        }

        public void AddMedicalRecord(MedicalRecord m)
        {
            _repo.AddMedicalRecord(m);
        }
    }
}