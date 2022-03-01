using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Permissions.Authorization;
using Permissions.Constants;
using Permissions.Data;
using Permissions.Models;
using Permissions.ViewModels;

namespace Permissions.Controllers
{
    public class WeaponController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WeaponController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Weapon
        [NeedPermission(Modules.Weapons, Operations.Read)]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Weapons.ToListAsync());
        }

        // GET: Weapon/Details/5
        [NeedPermission(Modules.Weapons, Operations.Read)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weapon = await _context.Weapons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weapon == null)
            {
                return NotFound();
            }

            return View(weapon);
        }

        // GET: Weapon/Create
        [NeedPermission(Modules.Weapons, Operations.Create)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Weapon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [NeedPermission(Modules.Weapons, Operations.Create)]
        public async Task<IActionResult> Create([Bind("Id,Name,Damage,Level")] Weapon weapon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(weapon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(weapon);
        }

        // GET: Weapon/Edit/5
        [NeedPermission(Modules.Weapons, Operations.Edit)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weapon = await _context.Weapons.FindAsync(id);
            if (weapon == null)
            {
                return NotFound();
            }
            return View(weapon);
        }

        // POST: Weapon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [NeedPermission(Modules.Weapons, Operations.Edit)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Damage,Level")] Weapon weapon)
        {
            if (id != weapon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weapon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeaponExists(weapon.Id))
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
            return View(weapon);
        }

        // GET: Weapon/Delete/5
        [NeedPermission(Modules.Weapons, Operations.Delete)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weapon = await _context.Weapons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weapon == null)
            {
                return NotFound();
            }

            return View(weapon);
        }

        // POST: Weapon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [NeedPermission(Modules.Weapons, Operations.Delete)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var weapon = await _context.Weapons.FindAsync(id);
            _context.Weapons.Remove(weapon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [NeedPermission(Modules.Weapons, Operations.Edit)]
        public async Task<IActionResult> EditPerks(int id)
        {
            if (id == null)
                return NotFound();

            if (_context.Weapons == null || _context.Perks == null)
                return NotFound();

            var allPerksAvailable = await _context.Perks.ToListAsync();
            var weapon = await _context.Weapons.Include(w => w.Perks).Where(w => w.Id == id).FirstOrDefaultAsync();

            // Create list of selected perks
            var selectedPerks = new List<PerkIsSelectedVM>();
            foreach (var perk in allPerksAvailable)
            {
                selectedPerks.Add(new PerkIsSelectedVM() { PerkId = perk.Id, PerkName = perk.Name, PerkIsSelected = false });
            }
            // Update list of selected perks with perks from entity
            foreach (var perk in selectedPerks)
            {
                if (weapon.Perks.Any(p => p.Id == perk.PerkId))
                {
                    perk.PerkIsSelected = true;
                }
            }

            var vm = new WeaponPerks();
            vm.WeaponId = weapon.Id;
            vm.Perks = selectedPerks;

            return View(vm);
        }

        [HttpPost, ActionName("EditPerks")]
        [NeedPermission(Modules.Weapons, Operations.Edit)]
        public async Task<IActionResult> EditPerksPost(WeaponPerks model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            var weapon = await _context.Weapons.Include(w => w.Perks).Where(w => w.Id == model.WeaponId).FirstOrDefaultAsync();
            if (weapon == null)
            {
                return RedirectToAction("Index");
            }
            // Delete all perks from weapon, they will be updated later on
            weapon.Perks = new List<Perk>();

            var allPerks = await _context.Perks.ToListAsync();
            foreach (var perk in allPerks)
            {
                if (model.Perks.Any(p => p.PerkName == perk.Name && p.PerkIsSelected == true))
                {
                    weapon.Perks.Add(perk);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        private bool WeaponExists(int id)
        {
            return _context.Weapons.Any(e => e.Id == id);
        }
    }
}
