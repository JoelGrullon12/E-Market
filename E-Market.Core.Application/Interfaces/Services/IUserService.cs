using E_Market.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.Interfaces.Services
{
    public interface IUserService : IGenericService<UserViewModel>
    {
        Task<UserViewModel> Login(LoginViewModel vm);
    }
}
