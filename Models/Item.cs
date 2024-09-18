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

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("categoryId")]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        public string? CategoryId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("subcategoryId")]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        public string SubCategoryId { get; set; } = null!;

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
