using CarteiraDeCambio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CarteiraDeCambio.Repositories
{
    public interface IAtivoCambialRepository
    {
        Task<IEnumerable<AtivoCambial>> GetAtivosCambiais();
        Task CreateAtivoCambial(AtivoCambial ativoCambial);


        //Task<TestObject> GetTestObject(string id);
        //Task<IEnumerable<TestObject>> GetTestObjectByName(string name);
        //Task<bool> UpdateTestObject(TestObject testObject);
        //Task<bool> DeleteTestObject(string id);
    }
}
