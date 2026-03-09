using System.ComponentModel.DataAnnotations;

namespace DotNetAssignmentMVC.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Department { get; set; }

        public int TotalLeaves { get; set; } = 20;

        public bool IsActive { get; set; } = true;
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(150, ErrorMessage = "Email cannot exceed 150 characters")]
        public string Email { get; set; }
    }
}
