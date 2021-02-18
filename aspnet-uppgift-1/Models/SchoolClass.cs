using aspnet_uppgift_1.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_uppgift_1.Models
{
    public class SchoolClass
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string TeacherId { get; set; }

        public virtual ICollection<SchoolClassStudent> SchoolClassStudents { get; set; }

        // Explicit omvandling fr. SC till SCVM
        //public static explicit operator SchoolClassViewModel(SchoolClass sc) 
        //    => new SchoolClassViewModel
        //    {
        //        Id = sc.Id,
        //        Name = sc.Name,
        //        Teacher = sc.Users.Where(user => user.Role,
        //        Students = sc.Students as List<ApplicationUser>
        //    };
    }
}
