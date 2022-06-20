using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Domain.Entities
{
    public class Advert
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string ImgUrl1 { get; set; }
        public string ImgUrl2 { get; set; }
        public string ImgUrl3 { get; set; }
        public string ImgUrl4 { get; set; }
        public DateTime PublishDate { get; set; }

        #region relations
        public int CategoryId { get; set; }
        public int UserId { get; set; }

        //Navigation Properties
        public Category Category { get; set; }
        public User User { get; set; }
        #endregion
    }
}
