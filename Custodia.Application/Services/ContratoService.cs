using Custodia.Application.Common.Interfaces;
using Custodia.Application.DTOs.Contratos;
using Custodia.Domain.Entities;

namespace Custodia.Application.Services
{
    public class ContratoService
    {
        private readonly IContratoRepository _repository;

        public ContratoService(IContratoRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Crea una nueva Contrato después de validar unicidad del anio.
        /// Devuelve el DTO con Id generado.
        /// </summary>
        public async Task<ContratoDto> CreateAsync(ContratoCreateDto dto)
        {
            // Validación de negocio: no duplicados de año
            if (await _repository.ExistsByCodeAsync(dto.Numero))
                throw new InvalidOperationException($"Ya existe una Contrato para el codigo {dto.Numero}.");

            var contrato = new Contrato
            {
                Numero = dto.Numero,
                DependenciaId = dto.DependenciaId,
                FechaFin = dto.FechaFin,
                FechaInicio = dto.FechaInicio,
            };

            var created = await _repository.AddAsync(contrato);

            return new ContratoDto { Id = created.Id, Numero = created.Numero, DependenciaId= created.DependenciaId};
        }

        /// <summary>
        /// Obtiene todas las Contratos
        /// </summary>
        public async Task<IEnumerable<ContratoDto>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return list.Select(v => new ContratoDto { Id = v.Id, Numero = v.Numero });
        }

        /// <summary>
        /// Obtiene por Id; retorna null si no existe
        /// </summary>
        public async Task<ContratoDto?> GetByIdAsync(int id)
        {
            var v = await _repository.GetByIdAsync(id);
            if (v == null) return null;
            return new ContratoDto { Id = v.Id, Numero = v.Numero };
        }

        /// <summary>
        /// Actualiza una Contrato. Valida existencia y duplicidad de año.
        /// </summary>
        public async Task UpdateAsync(int id, ContratoUpdateDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Contrato con id {id} no encontrada.");

            // Validar duplicado en otro registro
            if (await _repository.ExistsByCodeAsync(dto.Numero, excludingId: id))
                throw new InvalidOperationException($"Ya existe otra Contrato para el numero {dto.Numero}.");

            // Mapear cambios
            existing.Numero = dto.Numero;
            existing.FechaInicio = dto.FechaInicio;
            existing.FechaFin = dto.FechaFin;
            existing.Descripcion = dto.Descripcion;


            await _repository.UpdateAsync(existing);
        }

        /// <summary>
        /// Elimina una Contrato por id.
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Contrato con id {id} no encontrada.");

            // Si necesitas validar relaciones (ej. anaqueles), hazlo aquí antes de borrar.

            await _repository.DeleteAsync(existing);
        }
    }
}
