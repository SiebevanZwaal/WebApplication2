#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;

namespace WebApplication2.Controllers
{
    public class GroceriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GroceriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Groceries
        public async Task<IActionResult> Index()
        {
            return View(await _context.Grocerie.ToListAsync());
        }

        // GET: Groceries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grocerie = await _context.Grocerie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grocerie == null)
            {
                return NotFound();
            }

            return View(grocerie);
        }

        // GET: Groceries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Groceries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price")] Grocerie grocerie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grocerie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(grocerie);
        }

        // GET: Groceries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grocerie = await _context.Grocerie.FindAsync(id);
            if (grocerie == null)
            {
                return NotFound();
            }
            return View(grocerie);
        }

        // POST: Groceries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price")] Grocerie grocerie)
        {
            if (id != grocerie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grocerie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrocerieExists(grocerie.Id))
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
            return View(grocerie);
        }

        // GET: Groceries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grocerie = await _context.Grocerie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grocerie == null)
            {
                return NotFound();
            }

            return View(grocerie);
        }

        // POST: Groceries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grocerie = await _context.Grocerie.FindAsync(id);
            _context.Grocerie.Remove(grocerie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GrocerieExists(int id)
        {
            return _context.Grocerie.Any(e => e.Id == id);
        }
    }
}
