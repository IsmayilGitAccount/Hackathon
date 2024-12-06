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

        public DateTime StarDate { get; set; }
        public DateTime EndDate { get; set; }

        public double HourlyRate { get; set; }
        public byte BonusPercentage { get; set; }
        public int MonthlyMaxHours { get; set; }
    }
}
