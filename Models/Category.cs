using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Retail_Management_System.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(10)]
        public string CategoryCode { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        public int Status { get; set; } = 0;
        
        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        // Navigation property
        public virtual ICollection<Product>? Products { get; set; }
    }
}