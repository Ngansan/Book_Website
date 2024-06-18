using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebsiteBanSach.Data;
using WebsiteBanSach.Models;
using X.PagedList;

namespace WebsiteBanSach.Controllers.Admin
{
    public class AdminOrdersController : Controller
    {
        private readonly WebsiteBanSachContext _context;

        public AdminOrdersController(WebsiteBanSachContext context)
        {
            _context = context;
        }

        // GET: AdminOrders
        public IActionResult Index(int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            var websiteBanSachContext = _context.UserOrder.Include(u => u.Account)
                 .OrderByDescending(u => u.OrderDate);
            return View(websiteBanSachContext.ToList().ToPagedList((int)page, 10));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int id, bool IsPaid, int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            var userOrder = await _context.UserOrder.FirstOrDefaultAsync(u => u.OrderId == id);
            userOrder.IsPaid = IsPaid;
            await _context.SaveChangesAsync();
            var websiteBanSachContext = _context.UserOrder.Include(u => u.Account)
                .OrderByDescending(u => u.OrderDate);
            return View(websiteBanSachContext.ToList().ToPagedList((int)page, 10));
        }


        // GET: AdminOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserOrder == null)
            {
                return NotFound();
            }

            var userOrder = await _context.UserOrder
                .Include(u => u.Account)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (userOrder == null)
            {
                return NotFound();
            }

            return View(userOrder);
        }

        // GET: AdminOrders/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.Account, "AccountId", "FullName");
            return View();
        }

        // POST: AdminOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,OrderDate,ReceiverName,PhoneNumber,Address,PaymentMethod,Note,ShippingFee,TotalValue,IsDone,AccountId")] UserOrder userOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Account, "AccountId", "FullName", userOrder.AccountId);
            return View(userOrder);
        }

        // GET: AdminOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserOrder == null)
            {
                return NotFound();
            }

            var userOrder = await _context.UserOrder.FindAsync(id);
            if (userOrder == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Account, "AccountId", "FullName", userOrder.AccountId);
            return View(userOrder);
        }

        // POST: AdminOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,OrderDate,ReceiverName,PhoneNumber,Address,PaymentMethod,Note,ShippingFee,TotalValue,IsDone,AccountId")] UserOrder userOrder)
        {
            if (id != userOrder.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserOrderExists(userOrder.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Account, "AccountId", "FullName", userOrder.AccountId);
            return View(userOrder);
        }

        // GET: AdminOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserOrder == null)
            {
                return NotFound();
            }

            var userOrder = await _context.UserOrder
                .Include(u => u.Account)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (userOrder == null)
            {
                return NotFound();
            }

            return View(userOrder);
        }

        // POST: AdminOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserOrder == null)
            {
                return Problem("Entity set 'WebsiteBanSachContext.UserOrder'  is null.");
            }
            var userOrder = await _context.UserOrder.FindAsync(id);
            if (userOrder != null)
            {
                _context.UserOrder.Remove(userOrder);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserOrderExists(int id)
        {
            return (_context.UserOrder?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
