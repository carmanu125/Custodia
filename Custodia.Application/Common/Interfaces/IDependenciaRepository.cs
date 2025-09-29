using Custodia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodia.Application.Common.Interfaces
{
    public interface IDependenciaRepository
    {
        Task<Dependencia> AddAsync(Dependencia dependencia);
        Task<Dependencia?> GetByIdAsync(int id);
        Task<IEnumerable<Dependencia>> GetAllAsync();
        Task UpdateAsync(Dependencia dependencia);
        Task DeleteAsync(Dependencia dependencia);
        Task<bool> ExistsByNameAsync(string name, int? excludingId = null);
    }
}
