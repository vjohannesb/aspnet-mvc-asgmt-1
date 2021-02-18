using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspnet_uppgift_1.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace aspnet_uppgift_1.Areas.Identity.Pages.Account.Manage
{
    public class TwoFactorAuthenticationModel : PageModel
    {
        public IActionResult OnGet()
            => Redirect("/Identity/Account/Manage");
    }
}