using CarteiraDeCambio.Entities;
using MongoDB.Driver;

namespace CarteiraDeCambio.Data
{
    public interface IAtivoCambialContext
    {
        IMongoCollection<AtivoCambial> AtivosCambiais { get; }
    }
}