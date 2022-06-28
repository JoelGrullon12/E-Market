using E_Market.Core.Application.Helpers;
using E_Market.Core.Application.Interfaces.Repositories;
using E_Market.Core.Application.Interfaces.Services;
using E_Market.Core.Application.ViewModels.Adverts;
using E_Market.Core.Application.ViewModels.User;
using E_Market.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.Services
{
    public class AdvertService : IAdvertService
    {
        private readonly IAdvertRepository _adRepository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserViewModel _user;

        public AdvertService(IAdvertRepository adRepository, IHttpContextAccessor httpContext)
        {
            _adRepository = adRepository;
            _httpContext = httpContext;
            _user = _httpContext.HttpContext.Session.Get<UserViewModel>("user");
        }

        public async Task<List<AdvertViewModel>> GetAllViewModel()
        {
            var adList = await _adRepository.GetAllAsync();
            return adList.Select(t => new AdvertViewModel
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                ImgUrl1 = t.ImgUrl1,
                ImgUrl2 = t.ImgUrl2,
                ImgUrl3 = t.ImgUrl3,
                ImgUrl4 = t.ImgUrl4,
                Price = t.Price,
                CategoryId = t.CategoryId,
                PublishDate = t.PublishDate
            }).ToList();
        }

        public async Task<List<ShowAdvertViewModel>> GetForShowViewModel(bool areMines)
        {
            var adList = await _adRepository.GetAllWithIncludesAsync(new List<string>() { "Category" });

            adList = areMines ? adList.Where(ad => ad.UserId == _user.Id).ToList() 
                : adList.Where(ad => ad.UserId != _user.Id).ToList();

            return adList.Select(t => new ShowAdvertViewModel
            {
                Id = t.Id,
                ImgUrl = t.ImgUrl1,
                Name = t.Name,
                Price = t.Price,
                Description = Stuff.SetDescription(t.Description),
                Category = t.Category.Name
            }).ToList();
        }

        public async Task<AdvertViewModel> GetByIdViewModel(int id)
        {
            var ad = await _adRepository.GetByIdAsync(id);
            AdvertViewModel vm = new AdvertViewModel();
            vm.Id = ad.Id;
            vm.Name = ad.Name;
            vm.Description = ad.Description;
            vm.ImgUrl1 = ad.ImgUrl1;
            vm.ImgUrl2 = ad.ImgUrl2;
            vm.ImgUrl3 = ad.ImgUrl3;
            vm.ImgUrl4 = ad.ImgUrl4;
            vm.Price = ad.Price;
            vm.CategoryId = ad.CategoryId;
            vm.PublishDate = ad.PublishDate;

            return vm;
        }

        public async Task DML(AdvertViewModel vm, DMLAction action)
        {
            Advert ad;
            switch (action)
            {
                case DMLAction.Update:
                    ad = await _adRepository.GetByIdAsync(vm.Id);
                    break;

                default:
                    ad = new();
                    break;
            }

            ad.Id = vm.Id;
            ad.Name = vm.Name;
            ad.Description = vm.Description;
            ad.ImgUrl1 = vm.ImgUrl1;
            ad.ImgUrl2 = vm.ImgUrl2;
            ad.ImgUrl3 = vm.ImgUrl3;
            ad.ImgUrl4 = vm.ImgUrl4;
            ad.Price = vm.Price;
            ad.CategoryId = vm.CategoryId;
            ad.PublishDate = vm.PublishDate;
            ad.UserId = _user.Id;

            switch (action)
            {
                case DMLAction.Insert:
                    ad.PublishDate = DateTime.Now;
                    await _adRepository.AddAsync(ad);
                    break;

                case DMLAction.Update:
                    await _adRepository.UpdateAsync(ad);
                    break;

                case DMLAction.Delete:
                    await _adRepository.DeleteAsync(ad);
                    break;
            }
        }

        public async Task<AdvertViewModel> Add(AdvertViewModel vm)
        {
            Advert ad = new();
            ad.Id = vm.Id;
            ad.Name = vm.Name;
            ad.Description = vm.Description;
            ad.ImgUrl1 = vm.ImgUrl1;
            ad.ImgUrl2 = vm.ImgUrl2;
            ad.ImgUrl3 = vm.ImgUrl3;
            ad.ImgUrl4 = vm.ImgUrl4;
            ad.Price = vm.Price;
            ad.CategoryId = vm.CategoryId;
            ad.PublishDate = DateTime.Now;
            ad.UserId = _user.Id;

            ad = await _adRepository.AddAsync(ad);

            AdvertViewModel adVM = new()
            {
                Id = ad.Id,
                Name = ad.Name,
                Description = ad.Description,
                ImgUrl1 = ad.ImgUrl1,
                ImgUrl2 = ad.ImgUrl2,
                ImgUrl3 = ad.ImgUrl3,
                ImgUrl4 = ad.ImgUrl4,
                Price = ad.Price,
                CategoryId = ad.CategoryId,
                PublishDate = ad.PublishDate
            };

            return adVM;
        }

        public async Task<AdvertDetailViewModel> GetDetailsViewModel(int id)
        {
            Advert ad = await _adRepository.GetByIdWithIncludeAsync(id, new List<string>() { "Category", "User" });
            AdvertDetailViewModel vm = new()
            {
                ImgUrl1 = ad.ImgUrl1,
                ImgUrls = new List<string>(),
                Name = ad.Name,
                Price = ad.Price,
                Description = ad.Description,
                Category = ad.Category.Name,
                PublishDate = ad.PublishDate,
                User = $"{ad.User.Name} {ad.User.LastName}",
                Email = ad.User.Email,
                Phone = ad.User.Phone
            };

            if (ad.ImgUrl2 != null && ad.ImgUrl2 != "")
            {
                vm.ImgUrls.Add(ad.ImgUrl2);
            }

            if (ad.ImgUrl3 != null && ad.ImgUrl3 != "")
            {
                vm.ImgUrls.Add(ad.ImgUrl3);
            }

            if (ad.ImgUrl4 != null && ad.ImgUrl4 != "")
            {
                vm.ImgUrls.Add(ad.ImgUrl4);
            }

            return vm;
        }
    }
}
