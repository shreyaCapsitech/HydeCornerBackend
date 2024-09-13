using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace HydeBack.Models
{
    public class SubCategory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("category")]
        [Required]
        public string Category { get; set; } = null!;

        [BsonElement("subcategoryname")]
        [Required]
        public string SubCategoryName { get; set; } = null!;

        [BsonElement("imageurl")]
        [Required]
        public string ImageUrl { get; set; } = null!;

        [BsonElement("desc")]
        [Required]
        public string Desc { get; set; } = null!;
    }
}
