using CourseProject.UserControls;
using System;
using System.Collections.Generic;

namespace CourseProject.Model;

public partial class Visit
{
    public int IdVisit { get; set; }

    public int? ServicesProvidedDuringVisit { get; set; }

    public int? DocId { get; set; }

    public int? NumberMedicalCard { get; set; }

    public DateTime? DateTimeVisit { get; set; }

    public int? TotalCostVisit { get; set; }

    public virtual Doctor? Doc { get; set; }

    public virtual Patient? NumberMedicalCardNavigation { get; set; }

    public virtual ICollection<ServiceRenderedDuringVisit> ServiceRenderedDuringVisits { get; set; } = new List<ServiceRenderedDuringVisit>();


}
