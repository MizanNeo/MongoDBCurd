using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoSOFT.Domain.DTO
{
    public class ProductDto
    {
        
        public string id { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
        public int productCategory { get; set; }
        public decimal productPrice { get; set; }
    }
}
