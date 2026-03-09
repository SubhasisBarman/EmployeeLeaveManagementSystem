using System.ComponentModel.DataAnnotations;

namespace DotNetAssignmentMVC.Models
{
    public class Users
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(200)]
        public string Password { get; set; }

        [Required]
        [StringLength(20)]
        public string Role { get; set; }  // Admin / Employee

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
