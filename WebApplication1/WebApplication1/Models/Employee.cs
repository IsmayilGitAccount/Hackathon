using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Employee : IdentityUser
{
    [StringLength(100)]
    public string? FullName { get; set; }
    public double? Salary { get; set; }
    public double? Bonus { get; set; }

    public int ContractId { get; set; }
    public Contract Contract { get; set; }
    public List<Payroll> Payrolls { get; set; }

    //relational

    public List<VacationRequests> Vacations { get; set; }


}
