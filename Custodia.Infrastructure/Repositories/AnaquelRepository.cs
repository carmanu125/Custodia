using Custodia.Application.Common.Interfaces;
using Custodia.Domain.Entities;
using Custodia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodia.Infrastructure.Repositories
{
    public class AnaquelRepository : IAnaquelRepository
    {
        private readonly CustodiaDbContext _context;

        public AnaquelRepository(CustodiaDbContext context)
        {
            _context = context;
        }

        public async Task<Anaquel> AddAsync(Anaquel anaquel)
        {
            _context.Anaqueles.Add(anaquel);
            await _context.SaveChangesAsync();
            return anaquel;
        }

        public async Task<Anaquel?> GetByIdAsync(int id)
        {
            return await _context.Anaqueles
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Anaquel>> GetAllAsync()
        {
            return await _context.Anaqueles
                .AsNoTracking()
                .OrderBy(a => a.Id)
                .ToListAsync();
        }

        public async Task UpdateAsync(Anaquel anaquel)
        {
            _context.Anaqueles.Update(anaquel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Anaquel anaquel)
        {
            _context.Anaqueles.Remove(anaquel);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByNombreAsync(string nombre, int vigenciaId, int? excludingId = null)
        {
            var q = _context.Anaqueles.AsQueryable()
                .Where(a => a.Nombre == nombre && a.VigenciaId == vigenciaId);

            if (excludingId.HasValue)
                q = q.Where(a => a.Id != excludingId.Value);

            return await q.AnyAsync();
        }
    }
}
