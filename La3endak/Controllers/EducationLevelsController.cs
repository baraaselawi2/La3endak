using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using La3endak.Models;

namespace La3endak.Controllers
{
    public class EducationLevelsController : Controller
    {
        private readonly ModelContext _context;

        public EducationLevelsController(ModelContext context)
        {
            _context = context;
        }

        // GET: EducationLevels
        public async Task<IActionResult> Index()
        {
            return View(await _context.EducationLevels.ToListAsync());
        }

        // GET: EducationLevels/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationLevel = await _context.EducationLevels
                .FirstOrDefaultAsync(m => m.LevelId == id);
            if (educationLevel == null)
            {
                return NotFound();
            }

            return View(educationLevel);
        }

        // GET: EducationLevels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EducationLevels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LevelId,LevelName,Description")] EducationLevel educationLevel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(educationLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(educationLevel);
        }

        // GET: EducationLevels/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationLevel = await _context.EducationLevels.FindAsync(id);
            if (educationLevel == null)
            {
                return NotFound();
            }
            return View(educationLevel);
        }

        // POST: EducationLevels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("LevelId,LevelName,Description")] EducationLevel educationLevel)
        {
            if (id != educationLevel.LevelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(educationLevel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducationLevelExists(educationLevel.LevelId))
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
            return View(educationLevel);
        }

        // GET: EducationLevels/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationLevel = await _context.EducationLevels
                .FirstOrDefaultAsync(m => m.LevelId == id);
            if (educationLevel == null)
            {
                return NotFound();
            }

            return View(educationLevel);
        }

        // POST: EducationLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var educationLevel = await _context.EducationLevels.FindAsync(id);
            if (educationLevel != null)
            {
                _context.EducationLevels.Remove(educationLevel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EducationLevelExists(decimal id)
        {
            return _context.EducationLevels.Any(e => e.LevelId == id);
        }
    }
}
