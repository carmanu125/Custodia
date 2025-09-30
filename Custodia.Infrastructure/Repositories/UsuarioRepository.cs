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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly CustodiaDbContext _context;

        public UsuarioRepository(CustodiaDbContext context)
        {
            _context = context;
        }

        // Agrega y guarda la entidad
        public async Task<Usuario> AddAsync(Usuario Usuario)
        {
            _context.Usuarios.Add(Usuario);
            await _context.SaveChangesAsync();
            return Usuario;
        }

        // Obtiene por Id (sin tracking para lecturas)
        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        // Lista todas las Usuarios
        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios
                .AsNoTracking()
                .OrderByDescending(v => v.Correo)
                .ToListAsync();
        }

        // Actualiza la entidad (se asume que el calling code ya la modificó)
        public async Task UpdateAsync(Usuario Usuario)
        {
            _context.Usuarios.Update(Usuario);
            await _context.SaveChangesAsync();
        }

        // Elimina la entidad
        public async Task DeleteAsync(Usuario Usuario)
        {
            _context.Usuarios.Remove(Usuario);
            await _context.SaveChangesAsync();
        }

        // Verifica si existe otra Usuario con el mismo anio (excluir Id para update)
        public async Task<bool> ExistsByCorreoAsync(string codigo, int? excludingId = null)
        {
            var q = _context.Usuarios.AsQueryable().Where(v => v.Correo == codigo);
            if (excludingId.HasValue)
                q = q.Where(v => v.Id != excludingId.Value);

            return await q.AnyAsync();
        }
    }
}
