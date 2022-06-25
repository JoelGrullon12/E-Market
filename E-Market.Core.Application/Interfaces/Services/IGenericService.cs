using E_Market.Core.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.Interfaces.Services
{
    public interface IGenericService<ViewModel>
        where ViewModel : class
    {
        Task<List<ViewModel>> GetAllViewModel();
        Task<ViewModel> GetByIdViewModel(int id);
        Task DML(ViewModel vm, DMLAction action);
    }
}
