using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vozilastiburek.Data.Migrations;
using Vozilastiburek.Models;

namespace Vozilastiburek.Controllers
{
    public class TabliceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TabliceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tablice
        public async Task<IActionResult> Index()
        {
              return _context.Tablice != null ? 
                          View(await _context.Tablice.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Tablice'  is null.");
        }

        // GET: Tablice/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tablice == null)
            {
                return NotFound();
            }

            var tablice = await _context.Tablice
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tablice == null)
            {
                return NotFound();
            }

            return View(tablice);
        }

        // GET: Tablice/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tablice/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NazivTablica,Oznaka")] Tablice tablice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tablice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tablice);
        }

        // GET: Tablice/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tablice == null)
            {
                return NotFound();
            }

            var tablice = await _context.Tablice.FindAsync(id);
            if (tablice == null)
            {
                return NotFound();
            }
            return View(tablice);
        }

        // POST: Tablice/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NazivTablica,Oznaka")] Tablice tablice)
        {
            if (id != tablice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tablice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TabliceExists(tablice.Id))
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
            return View(tablice);
        }

        // GET: Tablice/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tablice == null)
            {
                return NotFound();
            }

            var tablice = await _context.Tablice
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tablice == null)
            {
                return NotFound();
            }

            return View(tablice);
        }

        // POST: Tablice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tablice == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tablice'  is null.");
            }
            var tablice = await _context.Tablice.FindAsync(id);
            if (tablice != null)
            {
                _context.Tablice.Remove(tablice);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TabliceExists(int id)
        {
          return (_context.Tablice?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
