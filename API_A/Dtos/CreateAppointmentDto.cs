namespace API_A.Dtos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class CreateAppointmentDto
{
    [Required]
    public int AppointmentId { get; set; }

    [Required]
    public int PatientId { get; set; }

    [Required]
    public string Pwz { get; set; }

    [Required, MinLength(1)]
    public List<CreateAppointmentServiceDto> Services { get; set; }
}