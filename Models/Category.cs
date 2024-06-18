using System.ComponentModel.DataAnnotations;

namespace WebsiteBanSach.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string? CategoryName { get; set; }
    }
}
