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
    public class VigenciaRepository : IVigenciaRepository
    {
        private readonly CustodiaDbContext _context;

        public VigenciaRepository(CustodiaDbContext context)
        {
            _context = context;
        }

        // Agrega y guarda la entidad
        public async Task<Vigencia> AddAsync(Vigencia vigencia)
        {
            _context.Vigencias.Add(vigencia);
            await _context.SaveChangesAsync();
            return vigencia;
        }

        // Obtiene por Id (sin tracking para lecturas)
        public async Task<Vigencia?> GetByIdAsync(int id)
        {
            return await _context.Vigencias
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        // Lista todas las vigencias
        public async Task<IEnumerable<Vigencia>> GetAllAsync()
        {
            return await _context.Vigencias
                .AsNoTracking()
                .OrderByDescending(v => v.Anio)
                .ToListAsync();
        }

        // Actualiza la entidad (se asume que el calling code ya la modificó)
        public async Task UpdateAsync(Vigencia vigencia)
        {
            _context.Vigencias.Update(vigencia);
            await _context.SaveChangesAsync();
        }

        // Elimina la entidad
        public async Task DeleteAsync(Vigencia vigencia)
        {
            _context.Vigencias.Remove(vigencia);
            await _context.SaveChangesAsync();
        }

        // Verifica si existe otra vigencia con el mismo anio (excluir Id para update)
        public async Task<bool> ExistsByAnioAsync(int anio, int? excludingId = null)
        {
            var q = _context.Vigencias.AsQueryable().Where(v => v.Anio == anio);
            if (excludingId.HasValue)
                q = q.Where(v => v.Id != excludingId.Value);

            return await q.AnyAsync();
        }
    }
}
