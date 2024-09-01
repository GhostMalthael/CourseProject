using CourseProject.UserControls;
using System;
using System.Collections.Generic;

namespace CourseProject.Model;

public partial class Doctor
{
    public int IdDoctor { get; set; }

    public string? FullNameDoctor { get; set; }

    public int? DoctorSpecialization { get; set; }

    public int? NumberOffice { get; set; }

    public virtual Specialization? DoctorSpecializationNavigation { get; set; }

    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();

    public static DoctorUserControl id;
}
