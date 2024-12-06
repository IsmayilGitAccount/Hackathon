using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels.Login
{
    public class EmployeeCreateVM
    {
        [Required, MaxLength(30)]
        public string UserName { get; set; } = null!;
        [Required, MaxLength(30)]
        public string FullName { get; set; } = null!;
        [Required, EmailAddress]
        public string EmailAddress { get; set; } = null!;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string RePassword { get; set; }

    }
}
