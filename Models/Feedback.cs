﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteBanSach.Models
{
    public class Feedback
    {
        public Feedback() { }

        [Key]
        public int FeedbackId { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int Star { get; set; }
        [Required]
        public DateTime FeedbackDate { get; set; }
        [Required]
        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public Account? Account { get; set; }
		[Required]
		[ForeignKey("Product")]
		public int ProductId { get; set; }
		public Product? Product { get; set; }

	}
}
