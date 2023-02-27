using System;
using System.Collections.Generic;

namespace core__task.Models;

public partial class Doctor
{
    public int DoctorId { get; set; }

    public string? DoctorName { get; set; }

    public string? DoctorImg { get; set; }

    public string? DoctorEmail { get; set; }

    public int? ClinicId { get; set; }

    public virtual Clinic? Clinic { get; set; }
}
