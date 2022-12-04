using CarteiraDeCambio.Data;
using CarteiraDeCambio.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CarteiraDeCambio.Data.SaldoContext;

namespace CarteiraDeCambio.Repositories
{
    public class SaldoRepository : ISaldoRepository
    {
        private readonly ISaldoContext _context;

        public SaldoRepository(ISaldoContext context)
        {
            _context = context;
        }

        public async Task CreateSaldo(Saldo saldo)
        {
            await _context.Saldos.InsertOneAsync(saldo);
        }
        
        public void CreateSaldoSync(Saldo saldo)
        {
            _context.Saldos.InsertOneAsync(saldo);
        }

        public async Task<IEnumerable<Saldo>> GetSaldos()
        {
            return await _context.Saldos.Find(p => true).ToListAsync();
        }

        public IEnumerable<Saldo> GetSaldosSync()
        {
            return _context.Saldos.Find(p => true).ToList() as IEnumerable<Saldo>;
        }
        
        public async Task<Saldo> GetSaldoByIdMoeda(string IdMoeda)
        {
            return await _context.Saldos.Find(p => p.idMoeda == IdMoeda).FirstOrDefaultAsync();
        }

        public Saldo GetSaldoByIdMoedaSync(string IdMoeda)
        {
            return _context.Saldos.Find(p => p.idMoeda == IdMoeda).FirstOrDefault();
        }

        public async Task<bool> UpdateSaldo(Saldo msaldo)
        {
            var updateResult = await _context.Saldos.ReplaceOneAsync(
               filter: g => g.Id == msaldo.Id, replacement: msaldo);

            return updateResult.IsAcknowledged
              && updateResult.ModifiedCount > 0;
        }
    }
}
