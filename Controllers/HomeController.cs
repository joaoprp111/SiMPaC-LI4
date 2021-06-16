using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SiMPAC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SiMPAC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Privacy()
        {
            return View();
        }


        public ActionResult Login()
        {
            return View("Login");
        }

        public ActionResult Register()
        {
            return View();
        }

        public IActionResult Pantry()
        {

            return View("Pantry");
        }

        public IActionResult WeekPlan()
        {
            ViewData["Message"] = "Your application description page.";

            return View("WeekPlan");
        }

        public IActionResult Cookbook()
        {
            ViewData["Message"] = "Your application description page.";

            return View("Cookbook");
        }

        public IActionResult ShoppingList()
        {
            ViewData["Message"] = "Your application description page.";

            return View("ShoppingList");
        }

    }
}
