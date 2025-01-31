using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using La3endak.Models;

namespace La3endak.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly ModelContext _context;

        public SubjectsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Subjects
        public async Task<IActionResult> Index()
        {
            return View(await _context.Subjects.ToListAsync());
        }
     


        // GET: Subjects/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects
                .FirstOrDefaultAsync(m => m.SubjectId == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // GET: Subjects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subjects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubjectId,SubjectName,SubjectImage,ImageFile")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                // Handle the image upload if an image is provided
                if (subject.ImageFile != null && subject.ImageFile.Length > 0)
                {
                    var fileName = Path.GetFileName(subject.ImageFile.FileName);//~/images/yourimage.jpg
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "~/img", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await subject.ImageFile.CopyToAsync(stream);
                    }

                    // Save the image file name in the Subject model
                    subject.SubjectImage = fileName;
                }

                _context.Add(subject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        // GET: Subjects/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }

        // POST: Subjects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("SubjectId,SubjectName,SubjectImage,ImageFile")] Subject subject)
        {
            if (id != subject.SubjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Handle image upload if a new image is selected
                    if (subject.ImageFile != null && subject.ImageFile.Length > 0)
                    {
                        var fileName = Path.GetFileName(subject.ImageFile.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await subject.ImageFile.CopyToAsync(stream);
                        }

                        // Update the SubjectImage property with the new image file name
                        subject.SubjectImage = fileName;
                    }

                    _context.Update(subject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectExists(subject.SubjectId))
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
            return View(subject);
        }

        // GET: Subjects/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects
                .FirstOrDefaultAsync(m => m.SubjectId == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject != null)
            {
                _context.Subjects.Remove(subject);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectExists(decimal id)
        {
            return _context.Subjects.Any(e => e.SubjectId == id);
        }
    }
}