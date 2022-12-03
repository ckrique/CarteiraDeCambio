using CarteiraDeCambio.Entities;
using MongoDB.Driver;

namespace CarteiraDeCambio.Data
{
    public interface ISaldoContext
    {
        IMongoCollection<Saldo> Saldos { get; }
    }
}