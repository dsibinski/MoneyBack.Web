using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneyBack.Web.Data;
using MoneyBack.Web.Models;

namespace MoneyBack.Web.Controllers
{
    public class SpendingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public SpendingsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Spendings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Spendings.ToListAsync());
        }

        // GET: Spendings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spending = await _context.Spendings
                .FirstOrDefaultAsync(m => m.SpendingId == id);
            if (spending == null)
            {
                return NotFound();
            }

            return View(spending);
        }

        // GET: Spendings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Spendings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpendingId,Name,Date,TotalAmount")] Spending spending)
        {
            if (ModelState.IsValid)
            {
                spending.User = await _userManager.GetUserAsync(User);
                _context.Add(spending);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(spending);
        }

        // GET: Spendings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spending = await _context.Spendings.FindAsync(id);
            if (spending == null)
            {
                return NotFound();
            }
            return View(spending);
        }

        // POST: Spendings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpendingId,Name,Date,TotalAmount")] Spending spending)
        {
            if (id != spending.SpendingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(spending);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpendingExists(spending.SpendingId))
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
            return View(spending);
        }

        // GET: Spendings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spending = await _context.Spendings
                .FirstOrDefaultAsync(m => m.SpendingId == id);
            if (spending == null)
            {
                return NotFound();
            }

            return View(spending);
        }

        // POST: Spendings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var spending = await _context.Spendings.FindAsync(id);
            _context.Spendings.Remove(spending);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpendingExists(int id)
        {
            return _context.Spendings.Any(e => e.SpendingId == id);
        }
    }
}
