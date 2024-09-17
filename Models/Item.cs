using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace HydeBack.Models
{
    public class Item
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        public string? Id { get; set; }

        [BsonElement("category")]
        [Required]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        public string? Category { get; set; }

        [BsonElement("subcategory")]
        [Required]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        public string SubCategory { get; set; } = null!;

        [BsonElement("itemname")]
        [Required]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        public string ItemName { get; set; } = null!;

        [BsonElement("imageurl")]
        [Required]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        public string ImageUrl { get; set; } = null!;

        [BsonElement("desc")]
        [Required]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        public string Desc { get; set; } = null!;

        [BsonElement("price")]
        [Required]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        public string Price { get; set; } = null!;

    }
}
