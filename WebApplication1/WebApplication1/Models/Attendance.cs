namespace WebApplication1.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TotalHours { get; set; }
        public int OvertimeHours { get; set; }
        public int TotalMinutes { get; set; }
        public string EmployeeId { get; set; }  
        public Employee Employee { get; set; }

    }
}
