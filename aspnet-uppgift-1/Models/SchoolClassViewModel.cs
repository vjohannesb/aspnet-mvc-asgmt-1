using aspnet_uppgift_1.Data;
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

        public ApplicationUser Teacher { get; set; }

        public List<ApplicationUser> Students { get; set; }

        public IEnumerable<ApplicationUser> AllTeachers { get; set; }

        public IEnumerable<ApplicationUser> AllStudents { get; set; }

        // För att skicka med valda studenter + vald lärare i HttpPost
        public List<string> StudentIds { get; set; }

        public string TeacherId { get; set; }

        // Explicit omvandling fr. SCVM till SC
        //public static explicit operator SchoolClass(SchoolClassViewModel scvm)
        //    => new SchoolClass
        //    {
        //        Id = scvm.Id,
        //        Name = scvm.Name,
        //        Teacher = scvm.Teacher,
        //        Students = scvm.Students
        //    };
    }
}
