using CarteiraDeCambio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CarteiraDeCambio.Repositories
{
    public interface ISaldoRepository
    {
        public Task<IEnumerable<Saldo>> GetSaldos();
        public IEnumerable<Saldo> GetSaldosSync();
        public Task<Saldo> GetSaldoByIdMoeda(string Id);
        public Saldo GetSaldoByIdMoedaSync(string Id);
        public Task CreateSaldo(Saldo saldo);
        public void CreateSaldoSync(Saldo saldo);
        public Task<bool> UpdateSaldo(Saldo saldo);

        //Task<TestObject> GetTestObject(string id);
        //Task<IEnumerable<TestObject>> GetTestObjectByName(string name);        
        //Task<bool> DeleteTestObject(string id);
    }
}
