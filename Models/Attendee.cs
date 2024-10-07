using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace HydeBack.Models
{
    public class Attendee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        public string? Id { get; set; }

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

        [BsonElement("designation")]
        [Required]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        public string Designation { get; set; } = null!;

    }
}
