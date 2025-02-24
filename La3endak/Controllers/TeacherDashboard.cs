using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using La3endak.Models;
using System.Security.Claims;
using System.Linq;

namespace La3endak.Controllers
{
    
    public class TeacherDashboardController : Controller
    {
        private readonly ModelContext _context;

        public TeacherDashboardController(ModelContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var teacherEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(teacherEmail))
            {
                return RedirectToAction("Login", "LogReg");
            }

            var teacher = await _context.UserAccounts
                .FirstOrDefaultAsync(u => u.Email == teacherEmail && u.UserRole == "teacher");

            if (teacher == null)
            {
                return RedirectToAction("Login", "LogReg");
            }

            // Get approved orders for teacher's subject
            var approvedOrders = await _context.Orders
                .Include(o => o.Subject)
                .Where(o => o.Status == "approved" && o.Subject.SubjectName == teacher.TeacherSubject)
                .ToListAsync();

            // Calculate revenue metrics
            var totalHours = approvedOrders.Count;
            var totalRevenue = totalHours * (teacher.HourlyRate ?? 0);

            // ViewBag setup for display
            ViewBag.HourlyRate = teacher.HourlyRate?.ToString("C0") ?? "Not set";
            ViewBag.TotalHours = totalHours.ToString("N0");
            ViewBag.TotalRevenue = totalRevenue.ToString("C0"); // Corrected name
            ViewBag.TeacherSubject = teacher.TeacherSubject ?? "Your Subject";

            // Get status counts for teacher's subject
            ViewBag.ApprovedCount = approvedOrders.Count;
            ViewBag.RejectedCount = await _context.Orders
                .Include(o => o.Subject)
                .CountAsync(o => o.Status == "rejected" && o.Subject.SubjectName == teacher.TeacherSubject);
            ViewBag.PendingCount = await _context.Orders
                .Include(o => o.Subject)
                .CountAsync(o => o.Status == "pending" && o.Subject.SubjectName == teacher.TeacherSubject);

            // Get pending orders for teacher's subject
            var pendingOrders = await _context.Orders
                .Include(o => o.Subject)
                .Where(o => o.Status == "pending" && o.Subject.SubjectName == teacher.TeacherSubject)
                .Include(o => o.User)
                .ToListAsync();

            return View(pendingOrders);
        }
        [HttpPost]
        public async Task<IActionResult> ApproveOrder(decimal id) // Change parameter type
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                order.Status = "approved";
                _context.Update(order);
                await _context.SaveChangesAsync();
                TempData["StatusMessage"] = "Order approved successfully!";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RejectOrder(decimal id) // Change parameter type
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                order.Status = "rejected";
                _context.Update(order);
                await _context.SaveChangesAsync();
                TempData["StatusMessage"] = "Order rejected successfully!";
            }
            return RedirectToAction(nameof(Index));
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
                .FirstOrDefaultAsync(u => u.Email == adminEmail && u.UserRole == "teacher");

            if (admin == null)
            {
                return RedirectToAction("Login", "LogReg");
            }

            return View(admin);
        }
    }
}