using CarteiraDeCambio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CarteiraDeCambio.Repositories
{
    public interface ISaldoRepository
    {
        Task<IEnumerable<Saldo>> GetSaldos();
        Task<Saldo> GetSaldoByIdMoeda(string Id);
        Task CreateSaldo(Saldo saldo);
        Task<bool> UpdateSaldo(Saldo saldo);

        //Task<TestObject> GetTestObject(string id);
        //Task<IEnumerable<TestObject>> GetTestObjectByName(string name);        
        //Task<bool> DeleteTestObject(string id);
    }
}
