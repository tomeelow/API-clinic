namespace API_A.Dtos;
using System.ComponentModel.DataAnnotations;

public class CreateAppointmentServiceDto
{
    [Required]
    public string ServiceName { get; set; }

    [Required, Range(0, double.MaxValue)]
    public decimal ServiceFee { get; set; }
}