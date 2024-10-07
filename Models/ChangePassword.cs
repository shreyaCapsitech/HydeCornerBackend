using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace HydeBack.Models
{
    public class ChangePassword
    {
        [BsonElement("username")]
        [Required]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        public string Username { get; set; } = null!;

        [BsonElement("oldPassword")]
        [Required]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        public string OldPassword { get; set; } = null!;

        [BsonElement("newPassword")]
        [Required]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        public string NewPassword { get; set; } = null!;
    }
}
