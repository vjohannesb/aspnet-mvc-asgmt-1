using aspnet_uppgift_1.Data;
using aspnet_uppgift_1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_uppgift_1.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public StudentController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
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

        // GET: Student/Classes
        public async Task<IActionResult> Classes()
        {
            var user = await _userManager.GetUserAsync(User);
            var enrolledIn = _context.SchoolClassStudents
                .Where(scs => scs.StudentId == user.Id)
                .Select(scs => scs.SchoolClass).ToList();

            var viewModels = new List<SchoolClassViewModel>();

            foreach (var schoolClass in enrolledIn)
            {
                var otherStudentIds = _context.SchoolClassStudents
                    .Where(scs => scs.SchoolClassId == schoolClass.Id)
                    .Select(scs => scs.StudentId).ToList();
                var otherStudents = await GetUsersFromIdAsync(otherStudentIds);

                viewModels.Add(new SchoolClassViewModel
                {
                    Id = schoolClass.Id,
                    Name = schoolClass.Name,
                    Teacher = await _userManager.FindByIdAsync(schoolClass.TeacherId),
                    Students = otherStudents
                });
            }

            return View(viewModels);
        }

        // GET: Student/ClassDetails/5
        public async Task<IActionResult> ClassDetails(int id)
        {
            var schoolClass = _context.SchoolClasses.FirstOrDefault(sc => sc.Id == id);
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

        /* Placeholders */
        public IActionResult Grades()
        {
            return View();
        }

        public IActionResult Schedule()
        {
            return View();
        }

        public IActionResult Library()
        {
            return View();
        }

        public IActionResult Internship()
        {
            return View();
        }

    }
}
