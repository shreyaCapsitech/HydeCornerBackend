using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace HydeBack.Models
{
    public class UserProfile
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        public string? Id { get; set; }

        [BsonElement("role")]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        public string? Role { get; set; } = null!;

        [BsonElement("username")]
        [Required]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        public string Username { get; set; } = null!;

        [BsonElement("password")]
        [Required]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        public string Password { get; set; } = null!;

        [BsonElement("name")]
        [Required]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        public string Name { get; set; } = null!;

        [BsonElement("gender")]
        [Required]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        public string Gender { get; set; } = null!;

        [BsonElement("age")]
        [Required]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        public string Age { get; set; } = null!;
    }
}
