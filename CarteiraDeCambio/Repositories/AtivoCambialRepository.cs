using CarteiraDeCambio.Data;
using CarteiraDeCambio.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CarteiraDeCambio.Data.AtivoCambialContext;

namespace TesteApiDocker2.Repositories
{
    public class AtivoCambialRepository : IAtivoCambialRepository
    {
        private readonly IAtivoCambialContext _context;

        public AtivoCambialRepository(IAtivoCambialContext context)
        {
            _context = context;
        }

        public async Task CreateAtivoCambial(AtivoCambial ativoCambial)
        {
            await _context.AtivosCambiais.InsertOneAsync(ativoCambial);
        }

        public async Task<IEnumerable<AtivoCambial>> GetAtivosCambiais()
        {
            return await _context.AtivosCambiais.Find(p => true).ToListAsync();
        }
    }
}
