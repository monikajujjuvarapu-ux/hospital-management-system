using Backend_API.models;
using Backend_API.repositories;
using Microsoft.AspNetCore.Mvc;

namespace Backend_API.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalRecordController : ControllerBase
    {
        private readonly MedicalRecordRepository _repo;

        public MedicalRecordController(MedicalRecordRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] MedicalRecord m)
        {
            try
            {
                _repo.AddMedicalRecord(m);
                return Ok("Record Added");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(_repo.GetAllMedicalRecords());
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] MedicalRecord m)
        {
            try
            {
                m.RecordId = id;
                _repo.UpdateMedicalRecord(m);
                return Ok("Updated");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}