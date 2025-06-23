using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Retail_Management_System.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;
        
        [Required]
        [StringLength(255)]
        public string Password { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;
        
        [Required]
        public int RoleID { get; set; }
        
        public int Status { get; set; } = 0;
        
        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        // Navigation property
        [ForeignKey("RoleID")]
        public virtual Role? Role { get; set; }
    }
}