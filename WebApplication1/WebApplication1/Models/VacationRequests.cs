namespace WebApplication1.Models
{
    public class VacationRequests
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Status { get; set; }
        public string Reason { get; set; }
        public Employee Employee { get; set; }
    }
}
