using System.ComponentModel.DataAnnotations;

namespace Tranning_Apps_Management.Models
{
    public class AddSignup
    {
        [Key]
        public string EmployeeId { get; set; } = null!;
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public string?DepID { get; set; }
        public string? Password { get; set; }
    }
}
