﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Employee : IdentityUser
{
    [StringLength(100)]
    public string? FullName { get; set; }

    public List<Contract>? Contracts { get; set; }
    public List<Payroll>? Payrolls { get; set; }

    //relational
    public List<VacationRequests>? Vacations { get; set; }


}
