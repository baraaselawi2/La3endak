using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using La3endak.Models;
using Microsoft.AspNetCore.Http;

namespace La3endak.Controllers
{
    public class AdminController : Controller
    {
        private readonly ModelContext _context;

        public AdminController(ModelContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Profile()
        {
            // Get current admin's email from session
            var adminEmail = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(adminEmail))
            {
                return RedirectToAction("Login", "LogReg");
            }

            // Get complete admin details with related data
            var admin = await _context.UserAccounts
                .FirstOrDefaultAsync(u => u.Email == adminEmail && u.UserRole == "admin");

            if (admin == null)
            {
                return RedirectToAction("Login", "LogReg");
            }

            return View(admin);
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.NumberOfUser = await _context.UserAccounts.CountAsync();
            ViewBag.NumberOfOrder = await _context.Orders.CountAsync();
            ViewBag.NumberOfReview = await _context.Testimonials.CountAsync();
            ViewBag.NumberOfTeacher = await _context.UserAccounts
              .Where(u => u.UserRole == "teacher")  // Adjust the condition to match your role system
              .CountAsync();
            ViewBag.NumberOfPendingOrder= await _context.Orders
            .Where(u => u.Status == "pending")  // Adjust the condition to match your role system
            .CountAsync();
            ViewBag.NumberOfapprovedOrder = await _context.Orders
          .Where(u => u.Status == "approved")  // Adjust the condition to match your role system
          .CountAsync();
            // Get current admin's email from session
            var adminEmail = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(adminEmail))
            {
                return RedirectToAction("Login", "LogReg");
            }

            // Get complete admin details with related data
            var admin = await _context.UserAccounts
                .FirstOrDefaultAsync(u => u.Email == adminEmail && u.UserRole == "admin");

            if (admin == null)
            {
                return RedirectToAction("Login", "LogReg");
            }

            return View(admin);
        }
    }
}