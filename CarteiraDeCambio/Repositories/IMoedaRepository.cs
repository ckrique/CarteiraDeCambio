using CarteiraDeCambio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace TesteApiDocker2.Repositories
{
    public interface IMoedaRepository
    {
        Task<IEnumerable<Moeda>> GetMoedas();
        Task CreateMoeda(Moeda moeda);
        Task<bool> UpdateMoeda(Moeda moeda);

        //Task<TestObject> GetTestObject(string id);
        //Task<IEnumerable<TestObject>> GetTestObjectByName(string name);        
        //Task<bool> DeleteTestObject(string id);
    }
}
