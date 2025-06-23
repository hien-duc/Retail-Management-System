using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Retail_Management_System.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(10)]
        public string ProductCode { get; set; } = string.Empty;
        
        [Required]
        [StringLength(200)]
        public string ProductName { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [Required]
        [StringLength(20)]
        public string SKU { get; set; } = string.Empty; // Stock Keeping Unit
        
        [Required]
        public int CategoryID { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal CostPrice { get; set; }
        
        public int StockQuantity { get; set; } = 0;
        
        [StringLength(255)]
        public string? ProductImage { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public int Status { get; set; } = 0;
        
        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        // Navigation property
        [ForeignKey("CategoryID")]
        public virtual Category? Category { get; set; }
    }
}