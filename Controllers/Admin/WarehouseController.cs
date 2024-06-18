using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebsiteBanSach.Data;
using WebsiteBanSach.Models;

namespace WebsiteBanSach.Controllers.Admin
{
    public class WarehouseController : Controller
    {
        private readonly WebsiteBanSachContext _context;

        public WarehouseController(WebsiteBanSachContext context)
        {
            _context = context;
        }

        // GET: Warehouse
        public async Task<IActionResult> Index()
        {
            var websiteBanSachContext = _context.Product.Include(p => p.Category);
            return View(await websiteBanSachContext.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int id, int newQuantity)
        {
            var product = await _context.Product.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductId == id);

            if (product != null)
            {
                product.Quantity = newQuantity;
                await _context.SaveChangesAsync();
            }
            var websiteBanSachContext = _context.Product.Include(p => p.Category);
            return View(await websiteBanSachContext.ToListAsync());
        }
    }
}
