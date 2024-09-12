using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace HydeBack.Models
{
    public class Login
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("role")]
        [Required]
        public string Role { get; set; } = null!;

        [BsonElement("username")]
        [Required]
        public string Username { get; set; } = null!;

        [BsonElement("password")]
        [Required]
        public string Password { get; set; } = null!;
    }
}
