namespace API_A.Models;
using System.Collections.Generic;

public class Doctor
{
    public int DoctorId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PWZ { get; set; }

    public ICollection<Appointment> Appointments { get; set; }
}