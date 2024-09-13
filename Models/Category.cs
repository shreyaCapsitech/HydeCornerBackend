using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace HydeBack.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("categoryname")]
        [Required]
        public string CategoryName { get; set; } = null!;

        [BsonElement("imageurl")]
        [Required]
        public string ImageUrl { get; set; } = null!;

        [BsonElement("desc")]
        [Required]
        public string Desc { get; set; } = null!;
    }
}
