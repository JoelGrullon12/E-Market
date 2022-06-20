using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.ViewModels.Adverts
{
    public class AdvertViewModel
    {
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage ="Debe colocarle un nombre al articulo")]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage ="Debe colocarle una descripcion al articulo")]
        public string Description { get; set; }

        #region Imagenes
        [DataType(DataType.Upload)]
        [Required(ErrorMessage ="Debe proporcionar al menos una imagen del articulo")]
        public string ImgUrl1 { get; set; }

        [DataType(DataType.Upload)]
        public string ImgUrl2 { get; set; }

        [DataType(DataType.Upload)]
        public string ImgUrl3 { get; set; }

        [DataType(DataType.Upload)]
        public string ImgUrl4 { get; set; }
        #endregion

        [DataType(DataType.Currency)]
        [Required(ErrorMessage ="Debe colocarle un precio al articulo")]
        public string Price { get; set; }

        [DataType(DataType.Custom)]
        [Required(ErrorMessage ="Debe seleccionar una categoria para el articulo")]
        public string CategoryId { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage ="Debe colocarle una fecha de creacion al articulo")]
        public string Creation { get; set; }


    }
}
