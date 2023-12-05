using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace NeoSOFT.Domain.Model
{

    public partial class Product
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
      
        public string id { get; set; } 
        [BsonElement("productName")]
        public string productName { get; set; }
        [BsonElement("productDescription")]
        public string productDescription { get; set; }
        [BsonElement("productCategory")]
        public int productCategory { get; set; }
        [BsonElement("productPrice")]
        public decimal productPrice { get; set; }
        public bool isActive { get; set; }  
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set;}
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set;}

    }
}
