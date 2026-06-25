using Backend_API.models;
using Backend_API.repositories;
using Microsoft.AspNetCore.Mvc;

namespace Backend_API.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly DoctorRepository _repo;

        public DoctorController(DoctorRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("add")]
        public IActionResult AddDoctor([FromBody] doctor d)
        {
            try
            {
                _repo.AddDoctor(d);
                return Ok("Doctor Added Successfully");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ✅ GET ALL DOCTORS
        [HttpGet("all")]
        public IActionResult GetAllDoctors()
        {
            var data = _repo.GetAllDoctors();
            return Ok(data);
        }
    }
}