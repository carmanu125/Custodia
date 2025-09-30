using Custodia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodia.Application.Common.Interfaces
{
    public interface IFolioRepository
    {
        Task<Folio> AddAsync(Folio folio);
        Task<Folio?> GetByIdAsync(int id);
        Task<IEnumerable<Folio>> GetAllAsync();
        Task UpdateAsync(Folio folio);
        Task DeleteAsync(Folio folio);
        Task<bool> ExistsByCodeAsync(int numero, int? excludingId = null);
    }
}
