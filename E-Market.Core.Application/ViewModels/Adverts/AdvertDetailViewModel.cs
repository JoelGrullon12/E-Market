using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.ViewModels.Adverts
{
    public class AdvertDetailViewModel
    {
        public string ImgUrl1 { get; set; }
        public List<string> ImgUrls { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime PublishDate { get; set; }
        public string User { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

    }
}
