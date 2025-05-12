namespace API_A.Models;
using System;
using System.Collections.Generic;

public class Patient
{
    public int PatientId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }

    public ICollection<Appointment> Appointments { get; set; }
}