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
        Task<Caja> CreateAsync(CreateCajaDto dto);
        Task<Caja?> GetByIdAsync(int id);
        Task<IEnumerable<Caja>> GetAllAsync();
        Task DeleteAsync(int id);
    }
}
