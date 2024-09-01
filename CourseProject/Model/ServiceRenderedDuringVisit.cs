using CourseProject.UserControls;
using System;
using System.Collections.Generic;

namespace CourseProject.Model;

public partial class ServiceRenderedDuringVisit
{
    public int Id { get; set; }

    public int? IdVisited { get; set; }

    public int? IdService { get; set; }

    public virtual PolyclinicService? IdServiceNavigation { get; set; }

    public virtual Visit? IdVisitedNavigation { get; set; }

    public static VisitUserControl visitIdSelect;

}
