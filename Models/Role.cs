using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Retail_Management_System.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string RoleName { get; set; } = string.Empty; // Admin, Manager, Staff
        
        public int Status { get; set; } = 0;
        
        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        // Navigation property
        public virtual ICollection<User>? Users { get; set; }
    }
}