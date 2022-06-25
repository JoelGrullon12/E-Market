using E_Market.Core.Application.Helpers;
using E_Market.Core.Application.Interfaces.Repositories;
using E_Market.Core.Application.Interfaces.Services;
using E_Market.Core.Application.ViewModels.Category;
using E_Market.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _catRepository;

        public CategoryService(ICategoryRepository catRepository)
        {
            _catRepository = catRepository;
        }

        public async Task<List<CategoryViewModel>> GetAllViewModel()
        {
            var catList = await _catRepository.GetAllAsync();
            return catList.Select(t => new CategoryViewModel
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description
            }).ToList();
        }

        public async Task<CategoryViewModel> GetByIdViewModel(int id)
        {
            var cat = await _catRepository.GetByIdAsync(id);
            CategoryViewModel vm = new CategoryViewModel();
            vm.Id = cat.Id;
            vm.Name = cat.Name;
            vm.Description = cat.Description;

            return vm;
        }

        public async Task DML(CategoryViewModel vm, DMLAction action)
        {
            Category cat = new();
            cat.Id=vm.Id;
            cat.Name = vm.Name;
            cat.Description = vm.Description;

            switch (action)
            {
                case DMLAction.Insert:
                    await _catRepository.AddAsync(cat);
                    break;

                case DMLAction.Update:
                    await _catRepository.UpdateAsync(cat);
                    break;

                case DMLAction.Delete:
                    await _catRepository.DeleteAsync(cat);
                    break;
            }
        }
    }
}
