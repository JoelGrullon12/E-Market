using E_Market.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.ViewModels.Category
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage ="Debe colocar un nombre para la categoria")]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Debe colocar una descripcion para la categoria")]
        public string Description { get; set; }

        public List<Advert> Adverts { get; set; }
        public int AdvertCount { get; set; }
        public int UserCount { get; set; }
    }
}
