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

        public SchoolClassesController(
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
                foreach(var id in ids)
                    _users.Add(await _userManager.FindByIdAsync(id));

                return _users;
            }
            return null;
        }

        // GET: SchoolClasses
        public async Task<IActionResult> Index()
        {
            var schoolClasses = await _context.SchoolClasses.ToListAsync();
            var schoolClassStudents = await _context.SchoolClassStudents.ToListAsync();

            var viewModels = new List<SchoolClassViewModel>();

            foreach (var sc in schoolClasses)
            {
                var studentIds = schoolClassStudents
                    .Where(scs => scs.SchoolClassId == sc.Id)
                    .Select(scs => scs.StudentId).ToList();

                viewModels.Add(new SchoolClassViewModel
                {
                    Id = sc.Id,
                    Name = sc.Name,
                    Teacher = await _userManager.FindByIdAsync(sc.TeacherId),
                    Students = await GetUsersFromIdAsync(studentIds)
                });
            }

            await SchoolClassViewModel.UpdateUserList(_userManager);
            return View(viewModels);
        }

        // GET: SchoolClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            var schoolClass = await _context.SchoolClasses
                .FirstOrDefaultAsync(m => m.Id == id);

            if (schoolClass == null)
                return RedirectToAction("Index");

            var enrolledStudentIds = _context.SchoolClassStudents
                .Where(scs => scs.SchoolClassId == id)
                .Select(scs => scs.StudentId).ToList();
            var enrolledStudents = await GetUsersFromIdAsync(enrolledStudentIds);

            var viewModel = new SchoolClassViewModel()
            {
                Id = schoolClass.Id,
                Name = schoolClass.Name,
                Teacher = await _userManager.FindByIdAsync(schoolClass.TeacherId),
                Students = enrolledStudents
            };

            return View(viewModel);
        }

        // GET: SchoolClasses/Create
        public async Task<IActionResult> Create()
        {
            await SchoolClassViewModel.UpdateUserList(_userManager);
            return View();
        }

        // POST: SchoolClasses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,TeacherId")] SchoolClassViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var schoolClass = new SchoolClass
                {
                    Id = viewModel.Id,
                    Name = viewModel.Name,
                    TeacherId = viewModel.TeacherId
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
                return RedirectToAction("Index");

            var schoolClass = await _context.SchoolClasses.FindAsync(id);

            if (schoolClass == null)
                return RedirectToAction("Index");

            var enrolledStudentIds = _context.SchoolClassStudents
                .Where(scs => scs.SchoolClassId == schoolClass.Id)
                .Select(scs => scs.StudentId).ToList();

            var enrolledStudents = await GetUsersFromIdAsync(enrolledStudentIds);

            var viewModel = new SchoolClassViewModel
            {
                Id = schoolClass.Id,
                Name = schoolClass.Name,
                Teacher = await _userManager.FindByIdAsync(schoolClass.TeacherId),
                TeacherId = schoolClass.TeacherId,
                Students = enrolledStudents,
                StudentIds = enrolledStudentIds
            };

            await SchoolClassViewModel.UpdateUserList(_userManager);
            return View(viewModel);
        }

        // POST: SchoolClasses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StudentIds,TeacherId")] SchoolClassViewModel viewModel)
        {
            if (id != viewModel.Id)
                return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                // Uppdatera tabell (SchoolClass)
                var schoolClass = new SchoolClass
                {
                    Id = id,
                    Name = viewModel.Name,
                    TeacherId = viewModel.TeacherId
                };

                try
                {
                    _context.Update(schoolClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoolClassExists(schoolClass.Id))
                        return NotFound();
                    else
                        throw;
                }


                // Uppdatera relation (SchoolClassStudents)
                var oldStudentList = await _context.SchoolClassStudents
                    .Where(scs => scs.SchoolClassId == id).ToListAsync();

                var updatedStudentList = viewModel.StudentIds?
                    .Select(studentId => new SchoolClassStudent
                    { StudentId = studentId, SchoolClassId = id })
                    .ToList();

                var studentsToRemove = new List<SchoolClassStudent>();

                if (updatedStudentList == null)
                    studentsToRemove = oldStudentList;
                else 
                    studentsToRemove = oldStudentList.Except(updatedStudentList).ToList();

                try
                {
                    if (studentsToRemove.Any())
                    {
                        foreach (var student in studentsToRemove)
                            _context.Remove(student);
                    }

                    if (updatedStudentList != null)
                    {
                        foreach (var newStudent in updatedStudentList)
                        {
                            if (SchoolClassStudentExists(newStudent.StudentId))
                                _context.Update(newStudent);
                            else
                                _context.Add(newStudent);
                        }
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoolClassExists(schoolClass.Id))
                        return NotFound();
                    else
                        throw;
                }


                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: SchoolClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            var schoolClass = await _context.SchoolClasses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schoolClass == null)
                return RedirectToAction("Index");

            var enrolledStudentIds = await _context.SchoolClassStudents
                .Where(scs => scs.SchoolClassId == id)
                .Select(scs => scs.StudentId).ToListAsync();
            var enrolledStudents = await GetUsersFromIdAsync(enrolledStudentIds);

            var viewModel = new SchoolClassViewModel
            {
                Id = schoolClass.Id,
                Name = schoolClass.Name,
                Teacher = await _userManager.FindByIdAsync(schoolClass.TeacherId),
                Students = enrolledStudents
            };

            return View(viewModel);
        }

        // POST: SchoolClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schoolClass = await _context.SchoolClasses.FindAsync(id);
            _context.SchoolClasses.Remove(schoolClass);

            var schoolClassStudent = _context.SchoolClassStudents.Where(scs => scs.SchoolClassId == id);
            foreach (var scs in schoolClassStudent)
                _context.SchoolClassStudents.Remove(scs);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchoolClassExists(int id)
        {
            return _context.SchoolClasses.Any(e => e.Id == id);
        }

        private bool SchoolClassStudentExists(string id)
        {
            return _context.SchoolClassStudents.Any(e => e.StudentId == id);
        }
    }
}
