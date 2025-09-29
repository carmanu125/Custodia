using Custodia.Application.Common.Interfaces;
using Custodia.Application.DTOs;
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
    public class CajaRepository : ICajaRepository
    {
        private readonly CustodiaDbContext _context;

        public CajaRepository(CustodiaDbContext context)
        {
            _context = context;
        }

        // Agrega y guarda la entidad
        public async Task<Caja> AddAsync(Caja caja)
        {
            _context.Cajas.Add(caja);
            await _context.SaveChangesAsync();
            return caja;
        }

        // Obtiene por Id (sin tracking para lecturas)
        public async Task<Caja?> GetByIdAsync(int id)
        {
            return await _context.Cajas
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        // Lista todas las cajas
        public async Task<IEnumerable<Caja>> GetAllAsync()
        {
            return await _context.Cajas
                .AsNoTracking()
                .OrderByDescending(v => v.Codigo)
                .ToListAsync();
        }

        // Actualiza la entidad (se asume que el calling code ya la modificó)
        public async Task UpdateAsync(Caja caja)
        {
            _context.Cajas.Update(caja);
            await _context.SaveChangesAsync();
        }

        // Elimina la entidad
        public async Task DeleteAsync(Caja caja)
        {
            _context.Cajas.Remove(caja);
            await _context.SaveChangesAsync();
        }

        // Verifica si existe otra caja con el mismo anio (excluir Id para update)
        public async Task<bool> ExistsByCodeAsync(string codigo, int? excludingId = null)
        {
            var q = _context.Cajas.AsQueryable().Where(v => v.Codigo == codigo);
            if (excludingId.HasValue)
                q = q.Where(v => v.Id != excludingId.Value);

            return await q.AnyAsync();
        }
    }
}
