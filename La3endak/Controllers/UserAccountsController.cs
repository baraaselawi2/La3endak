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
    public class UserAccountsController : Controller
    {
        private readonly ModelContext _context;

        public UserAccountsController(ModelContext context)
        {
            _context = context;
        }

        // GET: UserAccounts
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserAccounts.ToListAsync());
        }

        // GET: UserAccounts/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccount = await _context.UserAccounts
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userAccount == null)
            {
                return NotFound();
            }

            return View(userAccount);
        }

        // GET: UserAccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: UserAccounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,UserName,Email,Phone,Password,Address,UserRole,CreatedAt,UpdatedAt,Bio,StudentClass,TeacherSubject,Experience,HourlyRate,PreferredSubject,UserImage,Location, ImageFile")] UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                // Handle the image upload if an image is provided
                if (userAccount.ImageFile != null && userAccount.ImageFile.Length > 0)
                {
                    var fileName = Path.GetFileName(userAccount.ImageFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await userAccount.ImageFile.CopyToAsync(stream);
                    }

                    // Save the image file name in the UserAccount model
                    userAccount.UserImage = fileName;
                }

                _context.Add(userAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userAccount);
        }
        // GET: UserAccounts/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccount = await _context.UserAccounts.FindAsync(id);
            if (userAccount == null)
            {
                return NotFound();
            }

            return View(userAccount);
        }
        // POST: UserAccounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("UserId,UserName,Email,Phone,Password,Address,UserRole,CreatedAt,UpdatedAt,Bio,StudentClass,TeacherSubject,Experience,HourlyRate,PreferredSubject,UserImage,Location, ImageFile")] UserAccount userAccount)
        {
            if (id != userAccount.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Handle image upload if a new image is selected
                    if (userAccount.ImageFile != null && userAccount.ImageFile.Length > 0)
                    {
                        var fileName = Path.GetFileName(userAccount.ImageFile.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await userAccount.ImageFile.CopyToAsync(stream);
                        }

                        // Update the UserImage property with the new image file name
                        userAccount.UserImage = fileName;
                    }

                    _context.Update(userAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserAccountExists(userAccount.UserId))
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
            return View(userAccount);
        }

        // GET: UserAccounts/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccount = await _context.UserAccounts
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userAccount == null)
            {
                return NotFound();
            }

            return View(userAccount);
        }

        // POST: UserAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var userAccount = await _context.UserAccounts.FindAsync(id);
            if (userAccount != null)
            {
                _context.UserAccounts.Remove(userAccount);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserAccountExists(decimal id)
        {
            return _context.UserAccounts.Any(e => e.UserId == id);
        }
    }
}
