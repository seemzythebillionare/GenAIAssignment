using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GenAIAssignment.Data;
using GenAIAssignment.Models;

namespace GenAIAssignment.Controllers
{
    public class GenAIController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GenAIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GenAI
        public async Task<IActionResult> Index()
        {
              return _context.GenAI != null ? 
                          View(await _context.GenAI.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.GenAI'  is null.");
        }

        // GET: GenAI/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GenAI == null)
            {
                return NotFound();
            }

            var genAI = await _context.GenAI
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genAI == null)
            {
                return NotFound();
            }

            return View(genAI);
        }

        // GET: GenAI/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GenAI/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GenAIName,Summary,ImageFilename,AnchorLink,Like")] GenAI genAI)
        {
            if (ModelState.IsValid)
            {
                _context.Add(genAI);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(genAI);
        }

        // GET: GenAI/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GenAI == null)
            {
                return NotFound();
            }

            var genAI = await _context.GenAI.FindAsync(id);
            if (genAI == null)
            {
                return NotFound();
            }
            return View(genAI);
        }

        // POST: GenAI/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GenAIName,Summary,ImageFilename,AnchorLink,Like")] GenAI genAI)
        {
            if (id != genAI.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(genAI);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenAIExists(genAI.Id))
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
            return View(genAI);
        }

        // GET: GenAI/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GenAI == null)
            {
                return NotFound();
            }

            var genAI = await _context.GenAI
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genAI == null)
            {
                return NotFound();
            }

            return View(genAI);
        }

        // POST: GenAI/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GenAI == null)
            {
                return Problem("Entity set 'ApplicationDbContext.GenAI'  is null.");
            }
            var genAI = await _context.GenAI.FindAsync(id);
            if (genAI != null)
            {
                _context.GenAI.Remove(genAI);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenAIExists(int id)
        {
          return (_context.GenAI?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
