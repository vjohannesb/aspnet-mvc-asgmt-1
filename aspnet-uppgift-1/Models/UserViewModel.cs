using aspnet_uppgift_1.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_uppgift_1.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [MaxLength(50)]
        [DataType(DataType.Text)]
        [Display(Name = "First name")]
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(50, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [DataType(DataType.Text)]
        [Display(Name = "Last name")]
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(50, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid e-mail address.")]
        public string Email { get; set; }

        [Display(Name = "Role")]
        [Required(ErrorMessage = "{0} is required.")]
        public string Role { get; set; }

        [Display(Name = "User image")]
        public string ImageURI { get; set; }

        public string Username { get; set; }

        public SchoolClassViewModel SchoolClass { get; set; }
          
    }
}
