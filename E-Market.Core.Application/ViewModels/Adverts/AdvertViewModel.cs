using Microsoft.AspNetCore.Http;
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
        public string ImgUrl1 { get; set; }

        public string ImgUrl2 { get; set; }

        public string ImgUrl3 { get; set; }

        public string ImgUrl4 { get; set; }
        #endregion

        [Required(ErrorMessage ="Debe colocarle un precio al articulo")]
        public double Price { get; set; }

        [Required(ErrorMessage ="Debe seleccionar una categoria para el articulo")]
        public int CategoryId { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage ="Debe colocarle una fecha de creacion al articulo")]
        public DateTime PublishDate { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile Img1 { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile Img2 { get; set; }
        
        [DataType(DataType.Upload)]
        public IFormFile Img3 { get; set; }
        
        [DataType(DataType.Upload)]
        public IFormFile Img4 { get; set; }

    }
}
