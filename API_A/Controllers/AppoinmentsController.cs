using API_app_a.Repositories;

namespace API_A.Controllers;
using System;
using System.Linq;
using System.Threading.Tasks;
using Dtos;
using Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentRepository _repo;

        public AppointmentsController(IAppointmentRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointment(int id)
        {
            var appt = await _repo.GetAppointmentWithDetailsAsync(id);
            if (appt == null) return NotFound();

            var dto = new AppointmentDto
            {
                Date = appt.Date,
                Patient = new PatientDto
                {
                    FirstName = appt.Patient.FirstName,
                    LastName = appt.Patient.LastName,
                    DateOfBirth = appt.Patient.DateOfBirth
                },
                Doctor = new DoctorDto
                {
                    DoctorId = appt.Doctor.DoctorId,
                    Pwz = appt.Doctor.PWZ
                },
                AppointmentServices = appt.AppointmentServices
                    .Select(s => new AppointmentServiceDto
                    {
                        Name = s.Service.Name,
                        ServiceFee = s.ServiceFee
                    })
                    .ToList()
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _repo.AppointmentExistsAsync(dto.AppointmentId))
                return Conflict($"Appointment with ID {dto.AppointmentId} already exists.");

            var patient = await _repo.GetPatientByIdAsync(dto.PatientId);
            if (patient == null)
                return NotFound($"Patient with ID {dto.PatientId} not found.");

            var doctor = await _repo.GetDoctorByPwzAsync(dto.Pwz);
            if (doctor == null)
                return NotFound($"Doctor with PWZ {dto.Pwz} not found.");

            // validate and translate services
            var appointmentServices = new System.Collections.Generic.List<AppointmentService>();
            foreach (var s in dto.Services)
            {
                var serviceEntity = await _repo.GetServiceByNameAsync(s.ServiceName);
                if (serviceEntity == null)
                    return NotFound($"Service '{s.ServiceName}' not found.");

                appointmentServices.Add(new AppointmentService
                {
                    ServiceId = serviceEntity.ServiceId,
                    ServiceFee = s.ServiceFee
                });
            }

            var newAppointment = new Appointment
            {
                AppointmentId = dto.AppointmentId,
                PatientId = dto.PatientId,
                DoctorId = doctor.DoctorId,
                Date = DateTime.Now,
                AppointmentServices = appointmentServices
            };

            _repo.AddAppointment(newAppointment);

            if (!await _repo.SaveChangesAsync())
                return StatusCode(500, "An error occurred saving the appointment.");

            // build response DTO
            var resultDto = new AppointmentDto
            {
                Date = newAppointment.Date,
                Patient = new PatientDto
                {
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    DateOfBirth = patient.DateOfBirth
                },
                Doctor = new DoctorDto
                {
                    DoctorId = doctor.DoctorId,
                    Pwz = doctor.PWZ
                },
                AppointmentServices = dto.Services
                    .Select(s => new AppointmentServiceDto
                    {
                        Name = s.ServiceName,
                        ServiceFee = s.ServiceFee
                    })
                    .ToList()
            };

            return CreatedAtAction(
                nameof(GetAppointment),
                new { id = newAppointment.AppointmentId },
                resultDto);
        }
}