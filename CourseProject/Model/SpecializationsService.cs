using System;
using System.Collections.Generic;

namespace CourseProject.Model;

public partial class SpecializationsService
{
    public int Id { get; set; }

    public int? IdSpecialization { get; set; }

    public int? IdService { get; set; }

    public virtual PolyclinicService? IdServiceNavigation { get; set; }

    public virtual Specialization? IdSpecializationNavigation { get; set; }
}
