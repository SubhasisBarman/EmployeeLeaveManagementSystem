namespace DotNetAssignmentMVC.Models
{
    public class EmployeeDashboardViewModel
    {
        public string EmployeeName { get; set; }
        public int TotalLeaves { get; set; }
        public int PendingLeaves { get; set; }
        public int ApprovedLeaves { get; set; }
        public int RejectedLeaves { get; set; }

        public List<LeaveRequest> LeaveHistory { get; set; }
    }
}
