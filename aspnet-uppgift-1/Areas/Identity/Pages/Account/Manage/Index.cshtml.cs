using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using aspnet_uppgift_1.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace aspnet_uppgift_1.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string DisplayName { get; set; }

        public string Email { get; set; }

        public string ConfirmEmail { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "Email address")]
            [EmailAddress(ErrorMessage = "The e-mail address is not valid.")]
            public string Email { get; set; }

            [EmailAddress]
            [Display(Name = "Confirm email address")]
            [Compare("Email", ErrorMessage = "The email address and confirmation email address do not match.")]
            public string ConfirmEmail { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Email = email;
            DisplayName = user.DisplayName;

            Input = new InputModel
            {
                Email = email,
                ConfirmEmail = email,
                PhoneNumber = phoneNumber,
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            if (Input.Email != email)
            {
                if (_userManager.Users.Any(user => user.Email == Input.Email))
                {
                    StatusMessage = "Error: This e-mail address is already registered.";
                    return RedirectToPage();
                }

                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                var setUserNameResult = await _userManager.SetUserNameAsync(user, Input.Email);
                if (!setEmailResult.Succeeded || !setUserNameResult.Succeeded)
                {
                    StatusMessage = "Error: Unexpected error when trying to update e-mail address.";
                    return RedirectToPage();
                }
            }

            if (Input.PhoneNumber != phoneNumber)
            {
                if (_userManager.Users.Any(user => user.PhoneNumber == Input.PhoneNumber))
                {
                    StatusMessage = "Error: This phone number is already registered.";
                    return RedirectToPage();
                }

                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Error: Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
