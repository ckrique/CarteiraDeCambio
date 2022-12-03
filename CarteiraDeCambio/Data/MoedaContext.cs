using CarteiraDeCambio.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CarteiraDeCambio.Data
{

    public class MoedaContext : IMoedaContext
    {
        public IMongoCollection<Moeda> Moedas { get; }


        public MoedaContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>
               ("DatabaseSettings:ConnectionString"));

            var database = client.GetDatabase(configuration.GetValue<string>
               ("DatabaseSettings:DatabaseName"));

            Moedas = database.GetCollection<Moeda>(configuration.GetValue<string>
              ("DatabaseSettings:MoedasCollectionName"));
        }
        
    }

}
