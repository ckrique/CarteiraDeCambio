using CarteiraDeCambio.Entities;
using MongoDB.Driver;

namespace CarteiraDeCambio.Data
{
    public interface IMoedaContext
    {
        IMongoCollection<Moeda> Moedas { get; }
    }
}