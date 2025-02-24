using La3endak.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace La3endak.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;

        public HomeController(ILogger<HomeController> logger, ModelContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<IActionResult> Profile()
        {
            var studentEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(studentEmail))
            {
                return RedirectToAction("Login", "LogReg");
            }

            var student = await _context.UserAccounts
                .Include(u => u.Orders)
                    .ThenInclude(o => o.Subject)
                .FirstOrDefaultAsync(u => u.Email == studentEmail && u.UserRole == "student");

            return student == null ? RedirectToAction("Login", "LogReg") : View(student);
        }

        public async Task<IActionResult> About()
        {
            return View(await _context.AboutUs.ToListAsync());
        }

        public async Task<IActionResult> ContactUs()
        {
            return View(await _context.ContactUs.ToListAsync());
        }

        public IActionResult Create_us()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create_us([Bind("ContactId,Subject,ContactDate,FulluNmae,Email,Message")] ContactU contactU)
        {
            if (ModelState.IsValid)
            {
                contactU.ContactDate = DateTime.Now;
                _context.Add(contactU);
                await _context.SaveChangesAsync();
                TempData["MessageSent"] = "Your message has been sent successfully!";
                return RedirectToAction(nameof(ContactUs));
            }
            return View(contactU);
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.NumberOfSubject = await _context.Subjects.CountAsync(); 
            ViewBag.NumberOfTeacher = await _context.UserAccounts
            
           .Where(u => u.UserRole == "teacher")  // Adjust the condition to match your role system
           .CountAsync();
            ViewBag.NumberOfUser = await _context.UserAccounts.Where(u=>u.UserRole=="student").CountAsync();
            var studentEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(studentEmail))
            {
                return RedirectToAction("Login", "LogReg");
            }

            var modelTuple = new Tuple<List<Subject>, List<UserAccount>, List<Testimonial>, List<EducationLevel>>(
                await _context.Subjects.ToListAsync(),
                await _context.UserAccounts.Where(u => u.UserRole == "teacher").ToListAsync(),
                await _context.Testimonials.Include(t => t.User).ToListAsync(),
                await _context.EducationLevels.ToListAsync()
            );

            return View(modelTuple);
        }
        public async Task<IActionResult> Order()
        {
            var modelContext = _context.Orders.Include(o => o.Subject).Include(o => o.User);
            return View(await modelContext.ToListAsync());
        }
        public IActionResult Create()
        {
            var studentEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(studentEmail))
            {
                return RedirectToAction("Login", "LogReg");
            }

            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrder([Bind("OrderId,SubjectId,RequestedDate,RequestedTime,AdditionalNotes")] Order order)
        {
            var studentEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(studentEmail))
            {
                return RedirectToAction("Login", "LogReg");
            }

            var currentUser = await _context.UserAccounts
                .FirstOrDefaultAsync(u => u.Email == studentEmail && u.UserRole == "student");

            if (currentUser == null)
            {
                return RedirectToAction("Login", "LogReg");
            }

            // Set automatic values
            order.UserId = currentUser.UserId;
            order.Status = "pending";  // Default status

            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopulate dropdown if validation fails
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", order.SubjectId);
            return View("Create", order);
        }
        public async Task<IActionResult> TeacherProfile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Remove the Include() for non-navigation properties
            var teacher = await _context.UserAccounts
                .FirstOrDefaultAsync(u => u.UserId == id && u.UserRole == "teacher");

            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }
        public async Task<IActionResult> Subject()
        {
            return View(await _context.Subjects.ToListAsync());
        }
        public async Task<IActionResult> teacher()
        {
            // Fetch only the users who are part of the admin team
            var teamMembers = await _context.UserAccounts
                .Where(u => u.UserRole == "teacher") // Example condition
                .ToListAsync();

            return View(teamMembers);
        }
        // Other actions remain unchanged
        public IActionResult Privacy() => View();
        public IActionResult Service() => View();

        public async Task<IActionResult> Testimonials()
        {
            var testimonials = await _context.Testimonials
                .Include(t => t.User)
                .ToListAsync();
            return View(testimonials);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}