using Custodia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodia.Application.Common.Interfaces
{
    public interface IContratoRepository
    {
        Task<Contrato> AddAsync(Contrato contrato);
        Task<Contrato?> GetByNumeroAsync(string numero);
    }
}
