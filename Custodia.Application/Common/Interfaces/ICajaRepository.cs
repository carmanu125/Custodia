using Custodia.Application.DTOs;
using Custodia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodia.Application.Common.Interfaces
{
    public interface ICajaRepository
    {
        Task<Caja> AddAsync(Caja caja);
        Task<Caja?> GetByIdAsync(int id);
        Task<IEnumerable<Caja>> GetAllAsync();
        Task UpdateAsync(Caja caja);
        Task DeleteAsync(Caja caja);
        Task<bool> ExistsByCodeAsync(string code, int? excludingId = null);
    }
}
