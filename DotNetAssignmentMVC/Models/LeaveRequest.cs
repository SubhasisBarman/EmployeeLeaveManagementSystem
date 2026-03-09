using System.ComponentModel.DataAnnotations;

namespace DotNetAssignmentMVC.Models
{
    public class LeaveRequest
    {
        public int LeaveId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "From Date is required")]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }

        [Required(ErrorMessage = "To Date is required")]
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }

        [Required(ErrorMessage = "Reason is required")]
        [StringLength(500)]
        public string Reason { get; set; }

        public string Status { get; set; } = "Pending"; 

        public int? ApprovedBy { get; set; }

        public DateTime? ApprovedDate { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string EmployeeName { get; set; }
    }
}
