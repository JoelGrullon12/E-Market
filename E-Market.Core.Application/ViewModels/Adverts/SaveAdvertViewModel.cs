using E_Market.Core.Application.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.ViewModels.Adverts
{
    public class SaveAdvertViewModel
    {
        public List<CategoryViewModel> Categories { get; set; }
        public AdvertViewModel Advert { get; set; }
        
    }
}
