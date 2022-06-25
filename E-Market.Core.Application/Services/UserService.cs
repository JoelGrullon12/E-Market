using E_Market.Core.Application.Helpers;
using E_Market.Core.Application.Interfaces.Repositories;
using E_Market.Core.Application.Interfaces.Services;
using E_Market.Core.Application.ViewModels.User;
using E_Market.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserViewModel>> GetAllViewModel()
        {
            var userList = await _userRepository.GetAllAsync();
            return userList.Select(t => new UserViewModel
            {
                Id = t.Id,
                Name = t.Name,
                LastName = t.LastName,
                Email = t.Email,
                Phone = t.Phone,
                UserName = t.UserName,
                Password = t.Password
            }).ToList();
        }

        public async Task<UserViewModel> GetByIdViewModel(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            UserViewModel vm = new UserViewModel();
            vm.Id = user.Id;
            vm.Name = user.Name;
            vm.LastName = user.LastName;
            vm.Email = user.Email;
            vm.Phone = user.Phone;
            vm.UserName = user.UserName;
            vm.Password = user.Password;

            return vm;
        }

        public async Task DML(UserViewModel vm, DMLAction action)
        {
            User user = new();
            user.Id=vm.Id;
            user.Name = vm.Name;
            user.LastName = vm.LastName;
            user.Email = vm.Email;
            user.Phone = vm.Phone;
            user.UserName = vm.UserName;
            user.Password = Stuff.EncryptSHA256(vm.Password);

            switch (action)
            {
                case DMLAction.Insert:
                    await _userRepository.AddAsync(user);
                    break;

                case DMLAction.Update:
                    await _userRepository.UpdateAsync(user);
                    break;

                case DMLAction.Delete:
                    await _userRepository.DeleteAsync(user);
                    break;
            }
        }

        public async Task<UserViewModel> Login(LoginViewModel vm)
        {
            User user = await _userRepository.LoginAsync(vm);

            if (user == null)
                return null;

            UserViewModel userVM = new()
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                UserName = user.UserName,
                Password = user.Password
            };

            return userVM;
        }
    }
}
