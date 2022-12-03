using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CarteiraDeCambio.Entities
{    
    public class Saldo : BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("idMoeda")]
        public string? idMoeda { get; set; }

        [BsonElement("valor")]
        public decimal valor { get; set; }
    }
}
