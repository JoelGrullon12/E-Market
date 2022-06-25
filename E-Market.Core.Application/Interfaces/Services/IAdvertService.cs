using E_Market.Core.Application.ViewModels.Adverts;
using E_Market.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.Interfaces.Services
{
    public interface IAdvertService : IGenericService<AdvertViewModel>
    {
        Task<List<ShowAdvertViewModel>> GetForShowViewModel();
        Task<List<ShowAdvertViewModel>> GetMyAdvertsViewModel();
    }
}
