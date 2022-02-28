using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Permissions.Data;
using Permissions.Models;

namespace Permissions.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PerkController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PerkController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Perk
        public async Task<IActionResult> Index()
        {
            return View(await _context.Perks.ToListAsync());
        }

        // GET: Perk/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perk = await _context.Perks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (perk == null)
            {
                return NotFound();
            }

            return View(perk);
        }

        // GET: Perk/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Perk/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Bonus")] Perk perk)
        {
            if (ModelState.IsValid)
            {
                _context.Add(perk);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(perk);
        }

        // GET: Perk/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perk = await _context.Perks.FindAsync(id);
            if (perk == null)
            {
                return NotFound();
            }
            return View(perk);
        }

        // POST: Perk/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Bonus")] Perk perk)
        {
            if (id != perk.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(perk);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerkExists(perk.Id))
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
            return View(perk);
        }

        // GET: Perk/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perk = await _context.Perks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (perk == null)
            {
                return NotFound();
            }

            return View(perk);
        }

        // POST: Perk/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var perk = await _context.Perks.FindAsync(id);
            _context.Perks.Remove(perk);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerkExists(int id)
        {
            return _context.Perks.Any(e => e.Id == id);
        }
    }
}
