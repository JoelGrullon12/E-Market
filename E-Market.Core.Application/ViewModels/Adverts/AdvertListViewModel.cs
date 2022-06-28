using E_Market.Core.Application.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.ViewModels.Adverts
{
    public class AdvertListViewModel
    {
        public List<ShowAdvertViewModel> Adverts { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public string[] CatNames { get; set; }
        public List<bool> Selected { get; set; }
    }
}
