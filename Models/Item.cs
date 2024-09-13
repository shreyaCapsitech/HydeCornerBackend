using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace HydeBack.Models
{
    public class Item
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("itemname")]
        [Required]
        public string ItemName { get; set; } = null!;

        [BsonElement("category")]
        [Required]
        public string Category { get; set; } = null!;

        [BsonElement("subcategory")]
        [Required]
        public string SubCategory { get; set; } = null!;

        [BsonElement("imageurl")]
        [Required]
        public string ImageUrl { get; set; } = null!;

        [BsonElement("desc")]
        [Required]
        public string Desc { get; set; } = null!;

        [BsonElement("price")]
        [Required]
        public string Price { get; set; } = null!;

    }
}
