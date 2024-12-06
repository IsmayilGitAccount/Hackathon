using WebApplication1.Enums;

namespace WebApplication1.Models
{
    public class Payroll
    {
        public int Id { get; set; }
        public int Salary { get; set; }
        public int Bonus { get; set; }
        public Months Month { get; set; }
        public int Year { get; set; }
        public int Status { get; set; }
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
