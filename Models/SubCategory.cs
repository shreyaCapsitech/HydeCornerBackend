using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace HydeBack.Models
{
    public class SubCategory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        public string? Id { get; set; }

        [BsonElement("categoryId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        public string CategoryId { get; set; } = null!;

        [BsonElement("subcategoryname")]
        [Required]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        public string SubCategoryName { get; set; } = null!;

        [BsonElement("imageurl")]
        [Required]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        public string ImageUrl { get; set; } = null!;

        [BsonElement("desc")]
        [Required]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        public string Desc { get; set; } = null!;
    }
}
