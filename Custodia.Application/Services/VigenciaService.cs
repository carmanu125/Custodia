using Custodia.Application.Common.Interfaces;
using Custodia.Application.DTOs;
using Custodia.Application.DTOs.Vigencia;
using Custodia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodia.Application.Services
{
    public class VigenciaService
    {
        private readonly IVigenciaRepository _repository;

        public VigenciaService(IVigenciaRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Crea una nueva vigencia después de validar unicidad del anio.
        /// Devuelve el DTO con Id generado.
        /// </summary>
        public async Task<VigenciaDto> CreateAsync(VigenciaCreateDto dto)
        {
            // Validación de negocio: no duplicados de año
            if (await _repository.ExistsByAnioAsync(dto.Anio))
                throw new InvalidOperationException($"Ya existe una vigencia para el año {dto.Anio}.");

            var vigencia = new Vigencia
            {
                Anio = dto.Anio
            };

            var created = await _repository.AddAsync(vigencia);

            return new VigenciaDto { Id = created.Id, Anio = created.Anio };
        }

        /// <summary>
        /// Obtiene todas las vigencias
        /// </summary>
        public async Task<IEnumerable<VigenciaDto>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return list.Select(v => new VigenciaDto { Id = v.Id, Anio = v.Anio });
        }

        /// <summary>
        /// Obtiene por Id; retorna null si no existe
        /// </summary>
        public async Task<VigenciaDto?> GetByIdAsync(int id)
        {
            var v = await _repository.GetByIdAsync(id);
            if (v == null) return null;
            return new VigenciaDto { Id = v.Id, Anio = v.Anio };
        }

        /// <summary>
        /// Actualiza una vigencia. Valida existencia y duplicidad de año.
        /// </summary>
        public async Task UpdateAsync(int id, VigenciaUpdateDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Vigencia con id {id} no encontrada.");

            // Validar duplicado en otro registro
            if (await _repository.ExistsByAnioAsync(dto.Anio, excludingId: id))
                throw new InvalidOperationException($"Ya existe otra vigencia para el año {dto.Anio}.");

            // Mapear cambios
            existing.Anio = dto.Anio;

            await _repository.UpdateAsync(existing);
        }

        /// <summary>
        /// Elimina una vigencia por id.
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Vigencia con id {id} no encontrada.");

            // Si necesitas validar relaciones (ej. anaqueles), hazlo aquí antes de borrar.

            await _repository.DeleteAsync(existing);
        }
    }
}
