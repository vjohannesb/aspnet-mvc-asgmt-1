using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using aspnet_uppgift_1.Data;
using aspnet_uppgift_1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace aspnet_uppgift_1.Controllers
{
    [Authorize(Policy = "Admins")]
    public class SchoolClassesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SchoolClassesController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<List<ApplicationUser>> GetUsersFromId(List<string> ids)
        {
            if (ids != null)
            {
                var users = new List<ApplicationUser>();
                foreach(var id in ids)
                {
                    var _user = await _userManager.FindByIdAsync(id);
                    users.Add(_user);
                }
                return users;
            }
            return null;
        }

        // GET: SchoolClasses
        public async Task<IActionResult> Index()
        {
            var schoolClasses = await _context.SchoolClasses.ToListAsync();
            var viewModels = new List<SchoolClassViewModel>();
            var _allStudents = await _userManager.GetUsersInRoleAsync("Students");
            var _allTeachers = await _userManager.GetUsersInRoleAsync("Teacher");

            foreach (var sc in schoolClasses)
            {
                viewModels.Add(new SchoolClassViewModel
                {
                    Id = sc.Id,
                    Name = sc.Name,
                    Students = _allStudents?.Where(student => student.SchoolClassId == sc.Id).ToList(),
                    Teacher = _allTeachers?.FirstOrDefault(teacher => teacher.SchoolClassId == sc.Id)
                });
            }


            return View(viewModels);
        }

        // GET: SchoolClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var schoolClass = await _context.SchoolClasses
                .FirstOrDefaultAsync(m => m.Id == id);

            if (schoolClass == null)
            {
                return RedirectToAction("Index");
            }

            return View(schoolClass);
        }

        // GET: SchoolClasses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SchoolClasses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] SchoolClassViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var schoolClass = new SchoolClass()
                {
                    Id = viewModel.Id,
                    Name = viewModel.Name
                };

                _context.Add(schoolClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: SchoolClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var schoolClass = await _context.SchoolClasses.FindAsync(id);

            if (schoolClass == null)
                return NotFound();

            var viewModel = new SchoolClassViewModel
            {
                Id = schoolClass.Id,
                Name = schoolClass.Name,
                Students = schoolClass.Users?.Where(student => student.SchoolClassId == schoolClass.Id).ToList(),
                Teacher = schoolClass.Users?.FirstOrDefault(teacher => teacher.SchoolClassId == schoolClass.Id)
            };

            viewModel.AllStudents = await _userManager.GetUsersInRoleAsync("Student");
            viewModel.AllTeachers = await _userManager.GetUsersInRoleAsync("Teacher");

            return View(viewModel);
        }

        // POST: SchoolClasses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SchoolClassViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var users = new List<ApplicationUser>();

                if (viewModel.StudentIds != null)
                {
                    viewModel.Students = await GetUsersFromId(viewModel.StudentIds);
                    users = viewModel.Students;
                }

                if (viewModel.TeacherId != null)
                {
                    viewModel.Teacher = await _userManager.FindByIdAsync(viewModel.TeacherId);
                    users.Add(viewModel.Teacher);
                }

                //var schoolClass = (SchoolClass)viewModel;
                var schoolClass = new SchoolClass
                {
                    Id = viewModel.Id,
                    Name = viewModel.Name,
                    Users = users
                };

                try
                {
                    _context.Update(schoolClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoolClassExists(schoolClass.Id))
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
            return View(viewModel);
        }

        // GET: SchoolClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolClass = await _context.SchoolClasses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schoolClass == null)
            {
                return NotFound();
            }

            return View(schoolClass);
        }

        // POST: SchoolClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schoolClass = await _context.SchoolClasses.FindAsync(id);
            _context.SchoolClasses.Remove(schoolClass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchoolClassExists(int id)
        {
            return _context.SchoolClasses.Any(e => e.Id == id);
        }
    }
}
