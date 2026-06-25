using Backend_API.models;
using Backend_API.repositories;
using Microsoft.AspNetCore.Mvc;

namespace Backend_API.controllers
{
    [ApiController]

    [Route("api/[controller]")]

    public class HospitalController : ControllerBase
    {
        private readonly PatientRepository _patientRepo;

        private readonly DoctorRepository _doctorRepo;

        private readonly AppointmentRepository _appointmentRepo;

        private readonly MedicalRecordRepository _medicalRepo;

        public HospitalController
        (
            PatientRepository patientRepo,
            DoctorRepository doctorRepo,
            AppointmentRepository appointmentRepo,
            MedicalRecordRepository medicalRepo
        )
        {
            _patientRepo = patientRepo;

            _doctorRepo = doctorRepo;

            _appointmentRepo = appointmentRepo;

            _medicalRepo = medicalRepo;
        }

        [HttpPost("add-patient")]

        public IActionResult AddPatient(patient p)
        {
            _patientRepo.AddPatient(p);

            return Ok("Patient Added Successfully");
        }

        [HttpPost("add-doctor")]

        public IActionResult AddDoctor(doctor d)
        {
            _doctorRepo.AddDoctor(d);

            return Ok("Doctor Added Successfully");
        }

        [HttpPost("add-appointment")]

        public IActionResult AddAppointment(Appointment a)
        {
            _appointmentRepo.AddAppointment(a);

            return Ok("Appointment Added Successfully");
        }

        [HttpPost("add-medical-record")]

        public IActionResult AddMedicalRecord(MedicalRecord m)
        {
            _medicalRepo.AddMedicalRecord(m);

            return Ok("Medical Record Added Successfully");
        }
    }
}