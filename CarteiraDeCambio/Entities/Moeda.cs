using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CarteiraDeCambio.Entities
{    
    public class Moeda : BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        
        [BsonElement("sigla")]
        public string? sigla { get; set; }

        [BsonElement("nome")]
        public string? nome { get; set; }
    }
}
