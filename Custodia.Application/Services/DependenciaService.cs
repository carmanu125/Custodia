using Custodia.Application.Common.Interfaces;
using Custodia.Application.DTOs.Dependencia;
using Custodia.Domain.Entities;


namespace Custodia.Application.Services
{
    public class DependenciaService
    {
        private readonly IDependenciaRepository _repository;

        public DependenciaService(IDependenciaRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Crea una nueva Dependencia después de validar unicidad del anio.
        /// Devuelve el DTO con Id generado.
        /// </summary>
        public async Task<DependenciaDto> CreateAsync(DependenciaCreateDto dto)
        {
            // Validación de negocio: no duplicados de año
            if (await _repository.ExistsByNameAsync(dto.Nombre))
                throw new InvalidOperationException($"Ya existe una Dependencia para el nombre {dto.Nombre}.");

            var dependencia = new Dependencia
            {
                Nombre = dto.Nombre,
                Observaciones = dto.Observaciones,
                CajaId = dto.CajaId
            };

            var created = await _repository.AddAsync(dependencia);

            return new DependenciaDto { Id = created.Id, Nombre = created.Nombre, CajaId= created.CajaId, Observaciones = created.Observaciones};
        }

        /// <summary>
        /// Obtiene todas las Dependencias
        /// </summary>
        public async Task<IEnumerable<DependenciaDto>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return list.Select(v => new DependenciaDto { Id = v.Id, Nombre = v.Nombre, Observaciones = v.Observaciones });
        }

        /// <summary>
        /// Obtiene por Id; retorna null si no existe
        /// </summary>
        public async Task<DependenciaDto?> GetByIdAsync(int id)
        {
            var v = await _repository.GetByIdAsync(id);
            if (v == null) return null;
            return new DependenciaDto { Id = v.Id, Nombre = v.Nombre, Observaciones=v.Observaciones };
        }

        /// <summary>
        /// Actualiza una Dependencia. Valida existencia y duplicidad de nombre.
        /// </summary>
        public async Task UpdateAsync(int id, DependenciaUpdateDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Dependencia con id {id} no encontrada.");

            // Validar duplicado en otro registro
            if (await _repository.ExistsByNameAsync(dto.Nombre, excludingId: id))
                throw new InvalidOperationException($"Ya existe otra Dependencia para el nombre {dto.Nombre}.");

            // Mapear cambios
            existing.Nombre = dto.Nombre;
            existing.Observaciones = dto.Observaciones;

            await _repository.UpdateAsync(existing);
        }

        /// <summary>
        /// Elimina una Dependencia por id.
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Dependencia con id {id} no encontrada.");

            // Si necesitas validar relaciones (ej. anaqueles), hazlo aquí antes de borrar.

            await _repository.DeleteAsync(existing);
        }
    }
}
