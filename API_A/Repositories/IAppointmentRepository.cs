using API_A.Models;

namespace API_app_a.Repositories;

public interface IAppointmentRepository
{
    Task<Appointment> GetAppointmentWithDetailsAsync(int appointmentId);
    Task<bool> AppointmentExistsAsync(int appointmentId);
    Task<Patient> GetPatientByIdAsync(int patientId);
    Task<Doctor> GetDoctorByPwzAsync(string pwz);
    Task<Service> GetServiceByNameAsync(string name);

    void AddAppointment(Appointment appointment);
    Task<bool> SaveChangesAsync();
}