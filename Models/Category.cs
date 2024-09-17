using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace HydeBack.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        public string? Id { get; set; }

        [BsonElement("categoryname")]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        [Required]
        public string CategoryName { get; set; } = null!;

        [BsonElement("imageurl")]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        [Required]
        public string ImageUrl { get; set; } = null!;

        [BsonElement("desc")]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        [Required]
        public string Desc { get; set; } = null!;
    }
}
