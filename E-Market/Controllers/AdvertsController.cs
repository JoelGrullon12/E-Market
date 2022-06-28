using E_Market.Core.Application.Interfaces.Services;
using E_Market.Core.Application.ViewModels.Adverts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using E_Market.Core.Application.Helpers;
using E_Market.Middleware;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using E_Market.Core.Application.ViewModels.Category;
using System.Collections.Generic;

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

        public async Task<IActionResult> Index(string namesrch)
        {
            if (!_session.HasUser())
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            AdvertListViewModel vm = new();
            vm.Adverts = await _advertService.GetForShowViewModel(false);
            vm.Categories = await _catService.GetAllViewModel();

            vm.Selected = new List<bool>();

            int i = 0;
            foreach (CategoryViewModel cvm in vm.Categories)
            {
                vm.Selected.Add(false);
                i++;
            }

            if(namesrch==null)
                return View(vm);

            List<ShowAdvertViewModel> listVM = vm.Adverts;
            vm.Adverts = new();

            if (namesrch != null)
            {
                foreach(ShowAdvertViewModel ad in listVM)
                {
                    if (ad.Name.ToLower().Contains(namesrch.ToLower()))
                        vm.Adverts.Add(ad);
                }

                ViewData["search"] = namesrch;
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Index(AdvertListViewModel vm)
        {
            if (!_session.HasUser())
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            vm.Adverts = new List<ShowAdvertViewModel>();
            List<ShowAdvertViewModel> ads = await _advertService.GetForShowViewModel(false);
            vm.Categories = await _catService.GetAllViewModel();

            for(int i = 0; i < vm.Categories.Count; i++)
            {
                if (vm.Selected[i])
                {
                    foreach (ShowAdvertViewModel ad in ads)
                    {
                        if (ad.Category == vm.Categories[i].Name)
                        {
                            vm.Adverts.Add(ad);
                        }
                    }
                }
                
            }

            vm.CatNames = new string[vm.Categories.Count];

            return View(vm);
        }

        public async Task<IActionResult> MyAdverts()
        {
            if (!_session.HasUser())
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            AdvertListViewModel vm = new();
            vm.Adverts = await _advertService.GetForShowViewModel(true);
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Advert(int id)
        {
            if (!_session.HasUser())
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            return View(await _advertService.GetDetailsViewModel(id));
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
            vm.Advert.ImgUrl1 = vm.Advert.Img1.Name;
            AdvertViewModel adVM = await _advertService.Add(vm.Advert);

            if (adVM != null && adVM.Id != 0)
            {
                adVM.ImgUrl1 = UploadImg(vm.Advert.Img1, adVM.Id);
                adVM.ImgUrl2 = vm.Advert.Img2 != null ? UploadImg(vm.Advert.Img2, adVM.Id) : default;
                adVM.ImgUrl3 = vm.Advert.Img3 != null ? UploadImg(vm.Advert.Img3, adVM.Id) : default;
                adVM.ImgUrl4 = vm.Advert.Img4 != null ? UploadImg(vm.Advert.Img4, adVM.Id) : default;
                await _advertService.DML(adVM, DMLAction.Update);
            }

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

            AdvertViewModel adVM = await _advertService.GetByIdViewModel(vm.Advert.Id);
            vm.Advert.ImgUrl1 = UploadImg(vm.Advert.Img1, adVM.Id, true, adVM.ImgUrl1);
            vm.Advert.ImgUrl2 = UploadImg(vm.Advert.Img2, adVM.Id, true, adVM.ImgUrl2);
            vm.Advert.ImgUrl3 = UploadImg(vm.Advert.Img3, adVM.Id, true, adVM.ImgUrl3);
            vm.Advert.ImgUrl4 = UploadImg(vm.Advert.Img4, adVM.Id, true, adVM.ImgUrl4);

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

            string basePath = $"/img/ads/{vm.Id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (Directory.Exists(path))
            {
                DirectoryInfo dir = new(path);
                foreach(FileInfo file in dir.GetFiles())
                {
                    file.Delete();
                }

                foreach (DirectoryInfo file in dir.GetDirectories())
                {
                    file.Delete(true);
                }

                Directory.Delete(path);
            }

            return RedirectToRoute(new { controller = "Adverts", action = "MyAdverts" });
        }

        private string UploadImg(IFormFile file, int id, bool editMode=false,string imgUrl="")
        {
            if (editMode&&file==null)
            {
                return imgUrl;
            }
            string basePath = $"/img/ads/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string filename = guid + fileInfo.Extension;

            string filePath = Path.Combine(path, filename);

            using(var strem=new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(strem);
            }

            if (editMode)
            {
                string[] oldImgF = imgUrl.Split("/");
                string oldImg = oldImgF[^1];
                string oldPath = Path.Combine(path, oldImg);
                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);
            }

            return $"{basePath}/{filename}";
        }

    }
}
