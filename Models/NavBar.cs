using Microsoft.AspNetCore.Mvc;
using WebsiteBanSach.Data;

namespace WebsiteBanSach.Models
{
	public class NavBar: ViewComponent
	{
		private readonly WebsiteBanSachContext _context;

		public NavBar(WebsiteBanSachContext context)
		{
			_context = context;
		}

		public IViewComponentResult Invoke()
		{
			return View(_context.Category.ToList());
		}
	}
}
