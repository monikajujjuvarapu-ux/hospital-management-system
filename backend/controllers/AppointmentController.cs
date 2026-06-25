using Backend_API.models;
using Backend_API.repositories;
using Microsoft.AspNetCore.Mvc;

namespace Backend_API.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly AppointmentRepository _repo;

        public AppointmentController(AppointmentRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] Appointment a)
        {
            try
            {
                _repo.AddAppointment(a);
                return Ok("Appointment Booked");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(_repo.GetAllAppointments());
        }

        [HttpPut("cancel/{id}")]
        public IActionResult Cancel(int id)
        {
            try
            {
                _repo.CancelAppointment(id);
                return Ok("Cancelled");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}