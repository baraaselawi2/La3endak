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
    public class ContactUsController : Controller
    {
        private readonly ModelContext _context;

        public ContactUsController(ModelContext context)
        {
            _context = context;
        }

        // GET: ContactUs/this for user_page
        public async Task<IActionResult> Contact()
        {
            return View(await _context.ContactUs.ToListAsync());
        }

        // GET: ContactUs/Create/this for user_page
        public IActionResult Create_us()
        {
            return View();
        }

        // POST: ContactUs/Create/this for user_page
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create_us([Bind("ContactId,Subject,ContactDate,FulluNmae,Email,Message")] ContactU contactU)
        {
            if (ModelState.IsValid)
            {
                // Set the ContactDate to the current date and time
                contactU.ContactDate = DateTime.Now;

                _context.Add(contactU);
                await _context.SaveChangesAsync();

                // Set a success message in TempData
                TempData["MessageSent"] = "Your message has been sent successfully!";

                // Redirect to the Contact action after successful creation
                return RedirectToAction(nameof(Contact));
            }

            // If the model state is invalid, return to the Create view with the current model
            return View(contactU);
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.ContactUs.ToListAsync());
        }

        // GET: ContactUs/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactU = await _context.ContactUs
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (contactU == null)
            {
                return NotFound();
            }

            return View(contactU);
        }

        // GET: ContactUs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContactId,Subject,ContactDate,FulluNmae,Email")] ContactU contactU)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactU);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactU);
        }

        // GET: ContactUs/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactU = await _context.ContactUs.FindAsync(id);
            if (contactU == null)
            {
                return NotFound();
            }
            return View(contactU);
        }

        // POST: ContactUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("ContactId,Subject,ContactDate,FulluNmae,Email")] ContactU contactU)
        {
            if (id != contactU.ContactId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactU);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactUExists(contactU.ContactId))
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
            return View(contactU);
        }

        // GET: ContactUs/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactU = await _context.ContactUs
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (contactU == null)
            {
                return NotFound();
            }

            return View(contactU);
        }

        // POST: ContactUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var contactU = await _context.ContactUs.FindAsync(id);
            if (contactU != null)
            {
                _context.ContactUs.Remove(contactU);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactUExists(decimal id)
        {
            return _context.ContactUs.Any(e => e.ContactId == id);
        }
    }
}
