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
    public class FolioRepository : IFolioRepository
    {
        private readonly CustodiaDbContext _context;

        public FolioRepository(CustodiaDbContext context)
        {
            _context = context;
        }

        // Agrega y guarda la entidad
        public async Task<Folio> AddAsync(Folio folio)
        {
            _context.Folios.Add(folio);
            await _context.SaveChangesAsync();
            return folio;
        }

        // Obtiene por Id (sin tracking para lecturas)
        public async Task<Folio?> GetByIdAsync(int id)
        {
            return await _context.Folios
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        // Lista todas las Folios
        public async Task<IEnumerable<Folio>> GetAllAsync()
        {
            return await _context.Folios
                .AsNoTracking()
                .OrderByDescending(v => v.Numero)
                .ToListAsync();
        }

        // Actualiza la entidad (se asume que el calling code ya la modificó)
        public async Task UpdateAsync(Folio folio)
        {
            _context.Folios.Update(folio);
            await _context.SaveChangesAsync();
        }

        // Elimina la entidad
        public async Task DeleteAsync(Folio folio)
        {
            _context.Folios.Remove(folio);
            await _context.SaveChangesAsync();
        }

        // Verifica si existe otra Folio con el mismo anio (excluir Id para update)
        public async Task<bool> ExistsByCodeAsync(int numero, int? excludingId = null)
        {
            var q = _context.Folios.AsQueryable().Where(v => v.Numero == numero);
            if (excludingId.HasValue)
                q = q.Where(v => v.Id != excludingId.Value);

            return await q.AnyAsync();
        }
    }
}
