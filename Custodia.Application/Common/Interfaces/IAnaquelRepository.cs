using Custodia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodia.Application.Common.Interfaces
{
    public interface IAnaquelRepository
    {
        Task<Anaquel> AddAsync(Anaquel anaquel);
        Task<Anaquel?> GetByIdAsync(int id);
        Task<IEnumerable<Anaquel>> GetAllAsync();
        Task UpdateAsync(Anaquel anaquel);
        Task DeleteAsync(Anaquel anaquel);
        Task<bool> ExistsByNombreAsync(string nombre, int vigenciaId, int? excludingId = null);
    }
}
