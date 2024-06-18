using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebsiteBanSach.Models;

namespace WebsiteBanSach.Data
{
    public class WebsiteBanSachContext : DbContext
    {
        public WebsiteBanSachContext (DbContextOptions<WebsiteBanSachContext> options)
            : base(options)
        {
        }

        public DbSet<WebsiteBanSach.Models.Account> Account { get; set; } = default!;

        public DbSet<WebsiteBanSach.Models.Cart>? Cart { get; set; }

        public DbSet<WebsiteBanSach.Models.CartDetail>? CartDetail { get; set; }

        public DbSet<WebsiteBanSach.Models.Category>? Category { get; set; }

        public DbSet<WebsiteBanSach.Models.UserOrder>? UserOrder { get; set; }

        public DbSet<WebsiteBanSach.Models.OrderDetail>? OrderDetail { get; set; }

        public DbSet<WebsiteBanSach.Models.Product>? Product { get; set; }

        public DbSet<WebsiteBanSach.Models.Feedback>? Feedback { get; set; }
    }
}
