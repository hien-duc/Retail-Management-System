using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Retail_Management_System.Models
{
    public class Promotion
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; } = string.Empty;
        
        public string? Description { get; set; }
        
        public string? ImagePath { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public int Status { get; set; } = 0;
        
        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}