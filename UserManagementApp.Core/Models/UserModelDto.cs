using System.ComponentModel.DataAnnotations;

namespace UserManagementApp.Core.Models
{
    public class UserModelDto
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        
        [Required]
        [Phone]
        [StringLength(100)]
        public string PhoneNumber { get; set; }
        
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
    }
}