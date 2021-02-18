using aspnet_uppgift_1.Services.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_uppgift_1.Controllers
{
    [Authorize(Policy = "Admins")]
    public class AdminController : Controller
    {
        private readonly IIdentityService _identityService;

        public AdminController(IIdentityService identityService)
        {
            _identityService = identityService;
        }


        // GET: AdminController
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Users()
        {
            var userViewModels = await _identityService.GetAllUserViewModelsAsync();
            return View(userViewModels);
        }
    }
}
