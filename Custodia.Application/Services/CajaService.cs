using Custodia.Application.Common.Interfaces;
using Custodia.Application.DTOs.Caja;
using Custodia.Application.DTOs;
using Custodia.Domain.Entities;

namespace Custodia.Application.Services
{
    public class CajaService
    {

        private readonly ICajaRepository _repository;

        public CajaService(ICajaRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Crea una nueva Caja después de validar unicidad del anio.
        /// Devuelve el DTO con Id generado.
        /// </summary>
        public async Task<CajaDto> CreateAsync(CreateCajaDto dto)
        {
            // Validación de negocio: no duplicados de año
            if (await _repository.ExistsByCodeAsync(dto.Codigo))
                throw new InvalidOperationException($"Ya existe una Caja para el codigo {dto.Codigo}.");

            var Caja = new Caja
            {
                Codigo = dto.Codigo,
                AnaquelId = dto.AnaquelId
            };

            var created = await _repository.AddAsync(Caja);

            return new CajaDto { Id = created.Id, Codigo = created.Codigo, AnaquelId= created.AnaquelId };
        }

        /// <summary>
        /// Obtiene todas las Cajas
        /// </summary>
        public async Task<IEnumerable<CajaDto>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return list.Select(v => new CajaDto { Id = v.Id, Codigo = v.Codigo, AnaquelId = v.AnaquelId });
        }

        /// <summary>
        /// Obtiene por Id; retorna null si no existe
        /// </summary>
        public async Task<CajaDto?> GetByIdAsync(int id)
        {
            var v = await _repository.GetByIdAsync(id);
            if (v == null) return null;
            return new CajaDto { Id = v.Id, Codigo = v.Codigo, AnaquelId = v.AnaquelId };
        }

        /// <summary>
        /// Actualiza una Caja. Valida existencia y duplicidad de año.
        /// </summary>
        public async Task UpdateAsync(int id, CajaUpdateDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Caja con id {id} no encontrada.");

            // Validar duplicado en otro registro
            if (await _repository.ExistsByCodeAsync(dto.Codigo, excludingId: id))
                throw new InvalidOperationException($"Ya existe otra Caja para el codigo {dto.Codigo}.");

            // Mapear cambios
            existing.Codigo = dto.Codigo;

            await _repository.UpdateAsync(existing);
        }

        /// <summary>
        /// Elimina una Caja por id.
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Caja con id {id} no encontrada.");

            // Si necesitas validar relaciones (ej. anaqueles), hazlo aquí antes de borrar.

            await _repository.DeleteAsync(existing);
        }
    }
}
