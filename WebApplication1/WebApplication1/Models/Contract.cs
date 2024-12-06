namespace WebApplication1.Models;

public class Contract
{
    public int Id { get; set; }
    public double HourlyRate { get; set; }
    public byte BonusPercentage { get; set; }
    public int MonthlyMaxHours { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public Employee Employee { get; set; }
    public string EmployeeId { get; set; }
}
