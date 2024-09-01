using System;
using System.Collections.Generic;

namespace CourseProject.Model;

public partial class Patient
{
    public int NumberMedicalCardPatient { get; set; }

    public string? FullNamePatient { get; set; }

    public string? HomeAddressPatient { get; set; }

    public string? PhoneNumberPatient { get; set; }

    public DateOnly? BirthDatePatient { get; set; }

    public string? PassportDataPatient { get; set; }

    public string? MedicalPolicyPatient { get; set; }

    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
