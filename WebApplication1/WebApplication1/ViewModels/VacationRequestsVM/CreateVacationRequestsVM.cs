namespace WebApplication1.ViewModels.VacationRequests
{
    public class CreateVacationRequestsVM
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    }
}
