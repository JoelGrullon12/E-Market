using E_Market.Core.Application.Helpers;
using E_Market.Core.Application.Interfaces.Services;
using E_Market.Core.Application.ViewModels.User;
using E_Market.Models;
using E_Market.Core.Application.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using E_Market.Middleware;

namespace E_Market.Controllers
{
    public class HomeController : Controller
    {

        private readonly IUserService _userService;
        private readonly ValidateSession _session;

        public HomeController(IUserService userService, ValidateSession session)
        {
            _userService = userService;
            _session = session;
        }

        public IActionResult Index()
        {
            if(_session.HasUser())
                return RedirectToRoute(new { controller = "Adverts", action = "Index" });

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            UserViewModel user = await _userService.Login(vm);

            if (user != null)
            {
                HttpContext.Session.Set<UserViewModel>("user", user);
                return RedirectToRoute(new { controller = "Adverts", action = "Index" });
            }
            else
                ModelState.AddModelError("userValidation", "Usuario y/o contraseña incorrecta");

            return View(vm);
        }

        public IActionResult Register()
        {
            if (_session.HasUser())
                return RedirectToRoute(new { controller = "Adverts", action = "Index" });

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            HttpContext.Session.Set<UserViewModel>("user", vm);
            await _userService.DML(vm, DMLAction.Insert);
            return RedirectToRoute(new { controller = "Home", action = "LogOut" });
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
    }
}
