using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteBanSach.Data;
using WebsiteBanSach.Models;

namespace WebsiteBanSach.Controllers
{
	public class ProductByCategoryController : Controller
	{
		private readonly WebsiteBanSachContext _context;

		public ProductByCategoryController(WebsiteBanSachContext context)
		{
			_context = context;
		}
		public IActionResult Index(int? id)
		{
			var productByCategory = _context.Product
				.Where(x => x.CategoryId == id);
			return View(productByCategory.ToList());
		}
	}
}
