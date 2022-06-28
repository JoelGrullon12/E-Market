using E_Market.Core.Application.Helpers;
using E_Market.Core.Application.Interfaces.Services;
using E_Market.Core.Application.ViewModels.Category;
using E_Market.Core.Domain.Entities;
using E_Market.Middleware;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Market.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _catService;
        private readonly ValidateSession _session;

        public CategoriesController(ICategoryService categoryService, ValidateSession session)
        {
            _catService = categoryService;
            _session = session;
        }
        public async Task<IActionResult> Index()
        {
            if (!_session.HasUser())
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            List<CategoryViewModel> cats = await _catService.GetAllViewModel();
            List<CategoryViewModel> catVm = new();

            List<int> userIds= new List<int>();

            foreach(CategoryViewModel cat in cats)
            {
                foreach(Advert ad in cat.Adverts)
                {
                    if (!userIds.Contains(ad.UserId))
                    {
                        userIds.Add(ad.UserId);
                    }
                }

                cat.UserCount=userIds.Count;
                userIds = new();
                catVm.Add(cat);
            }

            return View(catVm);
        }

        public IActionResult Create()
        {
            if (!_session.HasUser())
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _catService.DML(vm, DMLAction.Insert);
            return RedirectToRoute(new { controller = "Categories", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_session.HasUser())
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            return View(await _catService.GetByIdViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _catService.DML(vm, DMLAction.Update);
            return RedirectToRoute(new { controller = "Categories", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_session.HasUser())
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            return View(await _catService.GetByIdViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CategoryViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _catService.DML(vm, DMLAction.Delete);
            return RedirectToRoute(new { controller = "Categories", action = "Index" });
        }
    }
}
