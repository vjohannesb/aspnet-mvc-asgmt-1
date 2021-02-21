using aspnet_uppgift_1.Data;
using aspnet_uppgift_1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_uppgift_1.Controllers
{
    [Authorize("Teachers")]
    public class TeacherController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TeacherController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsersFromIdAsync(List<string> ids)
        {
            if (ids != null)
            {
                var _users = new List<ApplicationUser>();
                foreach (var id in ids)
                    _users.Add(await _userManager.FindByIdAsync(id));
                return _users;
            }
            return null;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Teacher/Classes
        public async Task<IActionResult> Classes()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user.Role == "Student")
                return RedirectToAction("Classes", "Student");


            var enrolledIn = _context.SchoolClasses
                .Where(sc => sc.TeacherId == user.Id);

            var viewModels = new List<SchoolClassViewModel>();

            foreach (var schoolClass in enrolledIn)
            {

                viewModels.Add(new SchoolClassViewModel
                {
                    Id = schoolClass.Id,
                    Name = schoolClass.Name,
                    Teacher = user,
                });
            }

            return View(viewModels);
        }

        // GET: Teacher/ClassDetails/5
        public async Task<IActionResult> ClassDetails(int? id)
        {
            var schoolClass = _context.SchoolClasses.FirstOrDefault(sc => sc.Id == id);

            if (schoolClass == null)
                return RedirectToAction("Classes");

            var studentIds = _context.SchoolClassStudents
                .Where(scs => scs.SchoolClassId == id)
                .Select(scs => scs.StudentId).ToList();

            var viewModel = new SchoolClassViewModel
            {
                Id = schoolClass.Id,
                Name = schoolClass.Name,
                Teacher = await _userManager.FindByIdAsync(schoolClass.TeacherId),
                Students = await GetUsersFromIdAsync(studentIds)
            };

            return View(viewModel);
        }

        public IActionResult Schedule()
        {
            return View();
        }

        public IActionResult Grades()
        {
            return View();
        }

        public IActionResult Library()
        {
            return View();
        }
    }
}
