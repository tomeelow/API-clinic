namespace API_A.Models;
using System;
using System.Collections.Generic;

public class Appointment
{
    public int AppointmentId { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime Date { get; set; }

    public Patient Patient { get; set; }
    public Doctor Doctor { get; set; }
    public ICollection<AppointmentService> AppointmentServices { get; set; }
}