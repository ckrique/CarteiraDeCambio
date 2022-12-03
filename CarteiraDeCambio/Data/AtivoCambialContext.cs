using CarteiraDeCambio.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CarteiraDeCambio.Data
{

    public class AtivoCambialContext : IAtivoCambialContext
    {
        public IMongoCollection<AtivoCambial> AtivosCambiais { get; }

        public AtivoCambialContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>
               ("DatabaseSettings:ConnectionString"));

            var database = client.GetDatabase(configuration.GetValue<string>
               ("DatabaseSettings:DatabaseName"));

            AtivosCambiais = database.GetCollection<AtivoCambial>(configuration.GetValue<string>
              ("DatabaseSettings:AtivoMonetarioCollectionName"));
        }

        
    }

}
