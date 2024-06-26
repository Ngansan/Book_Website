﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebsiteBanSach.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string? ProductName { get; set; }

        [Required]
        public string? ProductDescription { get; set; }

        [Required]
        public string? PhotoURL { get; set; }

        [Required]
        public string? Origin { get; set; }

        [Required]
        public string? Branch { get; set; }

        [Required]
        public long Price { get; set; }
        public int Star { get; set; } = 0;

        [Required]
        public long Quantity { get; set; }

        public long QuantitySold { get; set; } = 0;

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
