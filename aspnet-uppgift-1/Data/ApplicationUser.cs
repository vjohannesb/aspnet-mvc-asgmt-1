using aspnet_uppgift_1.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_uppgift_1.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [PersonalData]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }

        [Required]
        [PersonalData]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string ImageURI { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Role { get; set; }

        [NotMapped]
        public string DisplayName => $"{FirstName} {LastName}"; 


        // Explicit omvandling fr. AU till UVM
        public static explicit operator UserViewModel(ApplicationUser au) 
            => new UserViewModel()
            {
                Id = au.Id,
                FirstName = au.FirstName,
                LastName = au.LastName,
                Email = au.Email,
                Username = au.Email,
                ImageURI = au.ImageURI
            };
    }
}
