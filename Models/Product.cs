using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace mongotest.Models
{

    public class Product
    {
        [BsonId]
        [BsonRepresentationAttribute(BsonType.ObjectId)]
        public string id {get; set;}



        [BsonElementAttribute("name")]
        [Required]
        public string Name { get; set; }

        [BsonElementAttribute("quantity")]
        [Required]
        public int? Quantity {get; set;}

        [BsonElementAttribute("price")]
        [Required]
        public int? Price { get; set; }
    }
}