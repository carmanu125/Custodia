using Custodia.Application.Common.Interfaces;
using Custodia.Domain.Entities;
using Custodia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Custodia.Infrastructure.Repositories
{
    public class DependenciaRepository : IDependenciaRepository
    {
        private readonly CustodiaDbContext _context;

        public DependenciaRepository(CustodiaDbContext context)
        {
            _context = context;
        }

        // Agrega y guarda la entidad
        public async Task<Dependencia> AddAsync(Dependencia Dependencia)
        {
            _context.Dependencias.Add(Dependencia);
            await _context.SaveChangesAsync();
            return Dependencia;
        }

        // Obtiene por Id (sin tracking para lecturas)
        public async Task<Dependencia?> GetByIdAsync(int id)
        {
            return await _context.Dependencias
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        // Lista todas las Dependencias
        public async Task<IEnumerable<Dependencia>> GetAllAsync()
        {
            return await _context.Dependencias
                .AsNoTracking()
                .OrderByDescending(v => v.Nombre)
                .ToListAsync();
        }

        // Actualiza la entidad (se asume que el calling code ya la modificó)
        public async Task UpdateAsync(Dependencia Dependencia)
        {
            _context.Dependencias.Update(Dependencia);
            await _context.SaveChangesAsync();
        }

        // Elimina la entidad
        public async Task DeleteAsync(Dependencia Dependencia)
        {
            _context.Dependencias.Remove(Dependencia);
            await _context.SaveChangesAsync();
        }

        // Verifica si existe otra Dependencia con el mismo anio (excluir Id para update)
        public async Task<bool> ExistsByNameAsync(string nombre, int? excludingId = null)
        {
            var q = _context.Dependencias.AsQueryable().Where(v => v.Nombre == nombre);
            if (excludingId.HasValue)
                q = q.Where(v => v.Id != excludingId.Value);

            return await q.AnyAsync();
        }
    }
}
