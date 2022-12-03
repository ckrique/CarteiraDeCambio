using CarteiraDeCambio.Data;
using CarteiraDeCambio.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CarteiraDeCambio.Data.MoedaContext;

namespace CarteiraDeCambio.Repositories
{
    public class MoedaRepository : IMoedaRepository
    {
        private readonly IMoedaContext _context;

        public MoedaRepository(IMoedaContext context)
        {
            _context = context;
        }

        public async Task CreateMoeda(Moeda moeda)
        {
            await _context.Moedas.InsertOneAsync(moeda);
        }

        public async Task<IEnumerable<Moeda>> GetMoedas()
        {
            return await _context.Moedas.Find(p => true).ToListAsync();
        }

        public async Task<Moeda> GetMoedaBySigla(string sigla)
        {
            return await _context.Moedas.Find(p => p.sigla == sigla).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateMoeda(Moeda moeda)
        {
            var updateResult = await _context.Moedas.ReplaceOneAsync(
               filter: g => g.Id == moeda.Id, replacement: moeda);

            return updateResult.IsAcknowledged
              && updateResult.ModifiedCount > 0;
        }
    }
}
