using E_Market.Core.Application.Interfaces.Services;
using E_Market.Core.Application.ViewModels.Adverts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using E_Market.Core.Application.Helpers;
using E_Market.Middleware;

namespace E_Market.Controllers
{
    public class AdvertsController : Controller
    {
        private readonly IAdvertService _advertService;
        private readonly ICategoryService _catService;
        private readonly SaveAdvertViewModel _saveAdvert;
        private readonly ValidateSession _session;

        public AdvertsController(IAdvertService advertService, ICategoryService categoryService, ValidateSession session)
        {
            _advertService = advertService;
            _catService = categoryService;
            _saveAdvert = new();
            _session = session;
        }

        public async Task<IActionResult> Index()
        {
            if (!_session.HasUser())
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            AdvertListViewModel vm = new();
            vm.Adverts = await _advertService.GetForShowViewModel();
            vm.Categories = await _catService.GetAllViewModel();
            return View(vm);
        }

        public async Task<IActionResult> MyAdverts()
        {
            if (!_session.HasUser())
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            AdvertListViewModel vm = new();
            vm.Adverts = await _advertService.GetForShowViewModel();
            return View(vm);
        }

        public IActionResult Advert()
        {
            if (!_session.HasUser())
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            return View();
        }

        public async Task<IActionResult> Create()
        {
            if (!_session.HasUser())
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            _saveAdvert.Categories = await _catService.GetAllViewModel();
            return View(_saveAdvert);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveAdvertViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Categories = await _catService.GetAllViewModel();
                return View(vm);
            }

            await _advertService.DML(vm.Advert, DMLAction.Insert);
            return RedirectToRoute(new { controller = "Adverts", action = "MyAdverts" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_session.HasUser())
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            SaveAdvertViewModel vm = new();
            vm.Advert = await _advertService.GetByIdViewModel(id);
            vm.Categories = await _catService.GetAllViewModel();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveAdvertViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Categories = await _catService.GetAllViewModel();
                return View(vm);
            }

            await _advertService.DML(vm.Advert, DMLAction.Update);
            return RedirectToRoute(new { controller = "Adverts", action = "MyAdverts" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_session.HasUser())
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            AdvertViewModel vm = new();
            vm = await _advertService.GetByIdViewModel(id);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(AdvertViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            await _advertService.DML(vm, DMLAction.Delete);
            return RedirectToRoute(new { controller = "Adverts", action = "MyAdverts" });
        }

    }
}
