using System;
using System.Collections.Generic;

namespace CourseProject.Model;

public partial class Account
{
    public int IdAccount { get; set; }

    public string? LoginAccount { get; set; }

    public string? PasswordAccount { get; set; }

    public int? RoleUser { get; set; }

    public virtual Role? RoleUserNavigation { get; set; }
}
