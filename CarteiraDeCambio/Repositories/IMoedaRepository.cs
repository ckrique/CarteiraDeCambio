using CarteiraDeCambio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CarteiraDeCambio.Repositories
{
    public interface IMoedaRepository
    {
        public Task<IEnumerable<Moeda>> GetMoedas();
        public IEnumerable<Moeda> GetMoedasSync();
        public Task<Moeda> GetMoedaBySigla(string sigla);
        public Moeda GetMoedaBySiglaSync(string sigla);
        public Task CreateMoeda(Moeda moeda);
        public void CreateMoedaSync(Moeda moeda);
        public Task<bool> UpdateMoeda(Moeda moeda);

        //Task<TestObject> GetTestObject(string id);
        //Task<IEnumerable<TestObject>> GetTestObjectByName(string name);        
        //Task<bool> DeleteTestObject(string id);
    }
}
