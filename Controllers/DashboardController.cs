using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebsiteBanSach.Data;
using WebsiteBanSach.Models;

namespace WebsiteBanSach.Controllers
{
    public class DashboardController : Controller
    {
        private readonly WebsiteBanSachContext _context;

        public DashboardController(WebsiteBanSachContext context)
        {
            _context = context;
        }

        // GET: Dashboard
        public async Task<IActionResult> Index()
        {
            var websiteBanSachContext = _context.OrderDetail.Include(o => o.Product).Include(o => o.UserOrder);
            var orderDetailList = _context.OrderDetail.Include(o=>o.Product).Include(o => o.UserOrder);
            long RevenueNovel = 0;
            long RevenueVN = 0;
            long RevenueManga = 0;
            long RevenueNN = 0;

            long [] RevenueByMonth = new long[12];
            long[] RevenueByMonthNovel = new long[12];
            long[] RevenueByMonthManga = new long[12];
            long[] RevenueByMonthVN = new long[12];
            long[] RevenueByMonthNN = new long[12];

            foreach (var item in orderDetailList)
            {
                if (item.UserOrder.OrderDate.Year == DateTime.Now.Year)
                {
                    if (item.Product.CategoryId == 1)
                    {
                        RevenueNovel += item.TotalPrice;
                        RevenueByMonthNovel[item.UserOrder.OrderDate.Month - 1] += item.TotalPrice;
                    }
                    else if (item.Product.CategoryId == 2)
                    {
                        RevenueManga += item.TotalPrice;
                        RevenueByMonthManga[item.UserOrder.OrderDate.Month - 1] += item.TotalPrice;
                    }
                    else if (item.Product.CategoryId == 3)
                    {
                        RevenueVN += item.TotalPrice;
                        RevenueByMonthVN[item.UserOrder.OrderDate.Month - 1] += item.TotalPrice;
                    }
                    else
                    {
                        RevenueNN += item.TotalPrice;
                        RevenueByMonthNN[item.UserOrder.OrderDate.Month - 1] += item.TotalPrice;
                    }

                    RevenueByMonth[item.UserOrder.OrderDate.Month - 1] += item.TotalPrice;
                }

            }

            double NovelRate = (double)RevenueNovel / (RevenueNovel + RevenueVN + RevenueManga + RevenueNN) * 100.0;
            double MangaRate = (double)RevenueManga / (RevenueNovel + RevenueVN + RevenueManga + RevenueNN) * 100.0;
            double VNRate = (double)RevenueVN / (RevenueNovel + RevenueVN + RevenueManga + RevenueNN) * 100.0;
            double NNRate = (double)RevenueNN / (RevenueNovel + RevenueVN + RevenueManga + RevenueNN) * 100.0;

            ViewData["NovelRate"] = NovelRate;
            ViewData["MangaRate"] = MangaRate;
            ViewData["VNRate"] = VNRate;
            ViewData["NNRate"] = NNRate;

            ViewData["RevenueNovel"] = RevenueNovel;
            ViewData["RevenueManga"] = RevenueManga;
            ViewData["RevenueVN"] = RevenueVN;
            ViewData["RevenueNN"] = RevenueNN;

            ViewData["RevenueByMonth"] = RevenueByMonth;

            ViewData["RevenueByMonthNovel"] = RevenueByMonthNovel;
            ViewData["RevenueByMonthManga"] = RevenueByMonthManga;
            ViewData["RevenueByMonthVN"] = RevenueByMonthVN;
            ViewData["RevenueByMonthNN"] = RevenueByMonthNN;

            return View(await websiteBanSachContext.ToListAsync());
        }
    }
}
