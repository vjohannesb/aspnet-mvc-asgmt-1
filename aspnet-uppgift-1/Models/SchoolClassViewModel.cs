using aspnet_uppgift_1.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_uppgift_1.Models
{
    public class SchoolClassViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "A class name is required.")]
        public string Name { get; set; }

        // Lärare ej "required" för att inte behöva tilldela lärare direkt
        [Display(Name = "Teacher")]
        public ApplicationUser Teacher { get; set; }

        public IEnumerable<ApplicationUser> Students { get; set; }

        // För att skicka med valda studenter + vald lärare i HttpPost
        public IEnumerable<string> StudentIds { get; set; }

        [Display(Name = "Teacher")]
        public string TeacherId { get; set; }


        public static IEnumerable<ApplicationUser> AllTeachers { get; set; }

        public static IEnumerable<ApplicationUser> AllStudents { get; set; }

        public static async Task UpdateUserList (UserManager<ApplicationUser> userManager)
        {
            AllTeachers = await userManager.GetUsersInRoleAsync("Teacher");
            AllTeachers = AllTeachers.OrderBy(user => user.FirstName);
            AllStudents = await userManager.GetUsersInRoleAsync("Student");
            AllStudents = AllStudents.OrderBy(user => user.FirstName);
        }

    }
}
