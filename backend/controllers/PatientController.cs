using Backend_API.models;
using Backend_API.repositories;
using Microsoft.AspNetCore.Mvc;

namespace Backend_API.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly PatientRepository _repo;

        public PatientController(PatientRepository repo)
        {
            _repo = repo;
        }

        // GET ALL PATIENTS
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var patients = _repo.GetAllPatients();
            return Ok(patients);
        }

        // ADD PATIENT
        [HttpPost("add")]
        public IActionResult AddPatient([FromBody] patient p)
        {
            try
            {
                _repo.AddPatient(p);
                return Ok("Patient Added Successfully");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // UPDATE PATIENT
        [HttpPut("update/{id}")]
        public IActionResult UpdatePatient(int id, [FromBody] patient p)
        {
            try
            {
                _repo.UpdatePatient(id, p);
                return Ok("Patient Updated Successfully");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE PATIENT
        [HttpDelete("delete/{id}")]
        public IActionResult DeletePatient(int id)
        {
            try
            {
                _repo.DeletePatient(id);
                return Ok("Patient Deleted Successfully");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}