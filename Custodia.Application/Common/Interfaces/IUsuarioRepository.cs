using Custodia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodia.Application.Common.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> AddAsync(Usuario usuario);
        Task<Usuario?> GetByIdAsync(int id);
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(Usuario usuario);
        Task<bool> ExistsByCorreoAsync(string correo, int? excludingId = null);
    }
}
