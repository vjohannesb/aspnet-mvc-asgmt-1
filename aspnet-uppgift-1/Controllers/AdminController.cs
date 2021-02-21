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
        // GET: AdminController
        public ActionResult Index()
        {
            return View();
        }

        /* Placeholders */
        public ActionResult Settings()
        {
            return View();
        }

        public ActionResult Schedules()
        {
            return View();
        }

        public ActionResult Grades()
        {
            return View();
        }

        public ActionResult Library()
        {
            return View();
        }
    }
}
