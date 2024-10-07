using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace HydeBack.Models
{


    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("userId")]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        public string? UserId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("itemId")]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        public List<string>? ItemId { get; set; }

        /* [BsonElement("items")]
         [BsonRequired]
         public List<OrderItem> Items { get; set; } = new List<OrderItem>();*/

        [BsonElement("quantities")]
        [BsonRequired] // Ensure quantities are present
        public List<int> Quantities { get; set; } = new List<int>(); // Initialize as an empty list

        [BsonElement("instruction")]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        public string Instruction { get; set; } = null!;

        [BsonElement("totalprice")]
        [BsonIgnoreIfNull, BsonIgnoreIfDefault]
        public string TotalPrice { get; set; } = null!;
    }

}
