using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vozilastiburek.Data;
using Vozilastiburek.Models;
using Microsoft.AspNetCore.Authorization;

namespace Vozilastiburek.Controllers
{
    public class VozilaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VozilaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vozila
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vozila.ToListAsync());
        }

        // GET: Jokes/ShowSearchForms
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // GET: Jokes/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.Vozila.Where(j => j.NazivVozila.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: Vozila/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vozila == null)
            {
                return NotFound();
            }

            var vozila = await _context.Vozila
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vozila == null)
            {
                return NotFound();
            }

            return View(vozila);
        }

        // GET: Vozila/Create

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vozila/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NazivVozila,Kolicina")] Vozila vozila)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vozila);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vozila);
        }

        [Authorize]
        // GET: Vozila/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vozila == null)
            {
                return NotFound();
            }

            var vozila = await _context.Vozila.FindAsync(id);
            if (vozila == null)
            {
                return NotFound();
            }
            return View(vozila);
        }

        [Authorize]
        // POST: Vozila/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Quantity")] Vozila vozila)
        {
            if (id != vozila.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vozila);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VozilaExists(vozila.Id))
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
            return View(vozila);
        }

        [Authorize]
        // GET: Vozila/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vozila == null)
            {
                return NotFound();
            }

            var vozila = await _context.Vozila
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vozila == null)
            {
                return NotFound();
            }

            return View(vozila);
        }

        [Authorize]
        // POST: Vozila/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vozila == null)
            {
                return Problem("Entity set 'AppDbContext.Vozila'  is null.");
            }
            var vozila = await _context.Vozila.FindAsync(id);
            if (vozila != null)
            {
                _context.Vozila.Remove(vozila);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VozilaExists(int id)
        {
            return _context.Vozila.Any(e => e.Id == id);
        }
    }
}
