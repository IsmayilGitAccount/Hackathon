﻿namespace WebApplication1.Models
{
    public class VacationRequests
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }

        //relational
        public Employee Employee { get; set; }

        public bool IsAccepted { get; set; }
    }
}
