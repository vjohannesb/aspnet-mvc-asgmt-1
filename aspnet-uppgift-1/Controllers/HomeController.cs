using aspnet_uppgift_1.Models;
using aspnet_uppgift_1.Services.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_uppgift_1.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IIdentityService _identityService;

        public HomeController(
            ILogger<HomeController> logger,
            IIdentityService identityService)
        {
            _logger = logger;
            _identityService = identityService;
        }

        public async Task<IActionResult> Index()
        {
            await _identityService.CreateRootAccountAsync();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
