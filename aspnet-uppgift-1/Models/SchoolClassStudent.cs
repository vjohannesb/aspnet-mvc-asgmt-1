using aspnet_uppgift_1.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_uppgift_1.Models
{
    public class SchoolClassStudent
    {
        [Key]
        public string StudentId { get; set; }

        public int SchoolClassId { get; set; }
        public virtual SchoolClass SchoolClass { get; set; }
    }
}
