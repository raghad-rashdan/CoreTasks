using System;
using System.Collections.Generic;

namespace admintaskcore.Models;

public partial class Clinic
{
    public int ClinicId { get; set; }

    public string? ClinicName { get; set; }

    public string? ClinicImg { get; set; }

    public string? ClinicDis { get; set; }

    public virtual ICollection<Doctor> Doctors { get; } = new List<Doctor>();
}
