namespace API_A.Dtos;
using System;
using System.Collections.Generic;

public class AppointmentDto
{
    public DateTime Date { get; set; }
    public PatientDto Patient { get; set; }
    public DoctorDto Doctor { get; set; }
    public List<AppointmentServiceDto> AppointmentServices { get; set; }
}