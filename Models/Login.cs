using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;


namespace HydeBack.Models
{
    public class Login
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
    }

}

