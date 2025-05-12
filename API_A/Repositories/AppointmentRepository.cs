using API_app_a.Repositories;

namespace API_A.Repositories;
using System.Threading.Tasks;
using Data;
using Models;
using Microsoft.EntityFrameworkCore;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly ClinicContext _context;

    public AppointmentRepository(ClinicContext context)
    {
        _context = context;
    }

    public async Task<Appointment> GetAppointmentWithDetailsAsync(int appointmentId)
    {
        return await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Include(a => a.AppointmentServices)
            .ThenInclude(asv => asv.Service)
            .SingleOrDefaultAsync(a => a.AppointmentId == appointmentId);
    }

    public Task<bool> AppointmentExistsAsync(int appointmentId)
    {
        return _context.Appointments.AnyAsync(a => a.AppointmentId == appointmentId);
    }

    public Task<Patient> GetPatientByIdAsync(int patientId)
    {
        return _context.Patients.FindAsync(patientId).AsTask();
    }

    public Task<Doctor> GetDoctorByPwzAsync(string pwz)
    {
        return _context.Doctors.SingleOrDefaultAsync(d => d.PWZ == pwz);
    }

    public Task<Service> GetServiceByNameAsync(string name)
    {
        return _context.Services.SingleOrDefaultAsync(s => s.Name == name);
    }

    public void AddAppointment(Appointment appointment)
    {
        _context.Appointments.Add(appointment);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}