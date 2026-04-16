using Backend.Data;
using Backend.Models;
using Backend.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AppointmentsController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;
        public AppointmentsController(UserManager<User> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }


        //create appointment controller
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAppointment(CreateAppointmentDto model)
        {
            //get logged in user
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Unauthorized();
            }

            //find patient profile
            var patient = await _context.Patients.FindAsync(user.Id);

            if (patient == null)
            {
                return BadRequest("user is not registered as a patient");
            }

            //validate provider exist
            var providerExist = await _context.Providers.AnyAsync(p => p.Id == model.ProviderId);


            //prevent double booking
            var conflict = await _context.Appointments.AnyAsync(a => 
            a.ProviderId == model.ProviderId
            && a.SlotDateTime == model.SlotDateTime
            && a.Status == AppointmentStatus.Approved);

            if(conflict)
            {
                return BadRequest("Time slot already booked");
            }

            //create appointment
            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                PatientId = patient.Id,
                ProviderId = model.ProviderId,
                SlotDateTime = model.SlotDateTime,
                VisitReason = model.VisitReason,
                RequestedAt = DateTime.UtcNow,
                Status = AppointmentStatus.Approved,
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return Ok(appointment);

        }

        //get my appointment
        [HttpGet("my")]
        [Authorize]
        public async Task<IActionResult> GetMyAppointments()
        {
            var user = await _userManager.GetUserAsync(User);

            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.UserId == user.Id);

            if (patient == null)
                return BadRequest();

            var appointments = await _context.Appointments
                .Where(a => a.PatientId == patient.Id)
                .Include(a => a.Provider)
                .ToListAsync();

            return Ok(appointments);
        }
    }
}
