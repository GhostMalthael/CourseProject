using System;
using System.Collections.Generic;

namespace CourseProject.Model;

public partial class Specialization
{
    public int IdSpecialization { get; set; }

    public string? NameSpecialization { get; set; }

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual ICollection<SpecializationsService> SpecializationsServices { get; set; } = new List<SpecializationsService>();
}
