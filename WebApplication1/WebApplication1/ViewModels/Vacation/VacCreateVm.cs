namespace WebApplication1.ViewModels.Vacation
{
    public class VacCreateVm
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
    }
}
