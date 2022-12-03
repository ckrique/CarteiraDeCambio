using CarteiraDeCambio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace TesteApiDocker2.Repositories
{
    public interface ISaldoRepository
    {
        Task<IEnumerable<Saldo>> GetTestObjects();
        Task CreateSaldo(Saldo saldo);
        Task<bool> UpdateSaldo(Saldo saldo);

        //Task<TestObject> GetTestObject(string id);
        //Task<IEnumerable<TestObject>> GetTestObjectByName(string name);        
        //Task<bool> DeleteTestObject(string id);
    }
}
