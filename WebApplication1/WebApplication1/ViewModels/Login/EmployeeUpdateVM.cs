using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels.Login
{
    public class EmployeeUpdateVM
    {
        public string FullName { get; set; }
        public double Salary { get; set; }
        public double Bonus { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string RePassword { get; set; }
        public string NewPassword { get; set; }
    }
}
