namespace DotNetAssignmentMVC.Models
{
    public class LeaveSummaryViewModel
    {
        public int LeaveId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
