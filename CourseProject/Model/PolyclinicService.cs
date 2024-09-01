using System;
using System.Collections.Generic;

namespace CourseProject.Model;

public partial class PolyclinicService
{
    public int IdPolyclinicServices { get; set; }

    public string? NamePolyclinicService { get; set; }

    public int? PriceService { get; set; }

    public virtual ICollection<ServiceRenderedDuringVisit> ServiceRenderedDuringVisits { get; set; } = new List<ServiceRenderedDuringVisit>();

    public virtual ICollection<SpecializationsService> SpecializationsServices { get; set; } = new List<SpecializationsService>();
}
