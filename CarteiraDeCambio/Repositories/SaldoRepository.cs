﻿using CarteiraDeCambio.Data;
using CarteiraDeCambio.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CarteiraDeCambio.Data.SaldoContext;

namespace TesteApiDocker2.Repositories
{
    public class SaldoRepository : ISaldoRepository
    {
        private readonly ISaldoContext _context;

        public SaldoRepository(ISaldoContext context)
        {
            _context = context;
        }

        public async Task CreateSaldo(Saldo msaldo)
        {
            await _context.Saldos.InsertOneAsync(msaldo);
        }

        public async Task<IEnumerable<Saldo>> GetTestObjects()
        {
            return await _context.Saldos.Find(p => true).ToListAsync();
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
