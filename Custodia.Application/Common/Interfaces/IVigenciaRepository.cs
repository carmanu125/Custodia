using Custodia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodia.Application.Common.Interfaces
{
    // Interfaz del repositorio para Vigencia
    public interface IVigenciaRepository
    {
        // Agrega una vigencia y guarda cambios
        Task<Vigencia> AddAsync(Vigencia vigencia);

        // Obtiene vigencia por Id
        Task<Vigencia?> GetByIdAsync(int id);

        // Obtiene todas las vigencias
        Task<IEnumerable<Vigencia>> GetAllAsync();

        // Actualiza la vigencia y guarda cambios
        Task UpdateAsync(Vigencia vigencia);

        // Elimina vigencia por Id (o por entidad) y guarda cambios
        Task DeleteAsync(Vigencia vigencia);

        // Verifica existencia por anio (para unicidad)
        Task<bool> ExistsByAnioAsync(int anio, int? excludingId = null);
    }
}
