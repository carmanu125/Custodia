using Custodia.Application.Common.Interfaces;
using Custodia.Domain.Entities;
using Custodia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Custodia.Infrastructure.Repositories
{
    public class ContratoRepository : IContratoRepository
    {
        private readonly CustodiaDbContext _context;

        public ContratoRepository(CustodiaDbContext context)
        {
            _context = context;
        }

        public async Task<Contrato> AddAsync(Contrato contrato)
        {
            _context.Contratos.Add(contrato);
            await _context.SaveChangesAsync();
            return contrato;
        }

        public async Task<Contrato?> GetByNumeroAsync(string numero)
        {
            return await _context.Contratos
                .FirstOrDefaultAsync(c => c.Numero == numero);
        }
    }
}
