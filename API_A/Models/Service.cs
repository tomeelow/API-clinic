namespace API_A.Models;
using System.Collections.Generic;

public class Service
{
    public int ServiceId { get; set; }
    public string Name { get; set; }
    public decimal BaseFee { get; set; }

    public ICollection<AppointmentService> AppointmentServices { get; set; }
}