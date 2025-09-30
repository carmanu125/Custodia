using Custodia.Application.Common.Interfaces;
using Custodia.Application.DTOs;
using Custodia.Application.DTOs.Folio;
using Custodia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodia.Application.Services
{
    public class FolioService
    {
        private readonly IFolioRepository _repository;

        public FolioService(IFolioRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Crea una nueva Folio después de validar unicidad del anio.
        /// Devuelve el DTO con Id generado.
        /// </summary>
        public async Task<FolioDto> CreateAsync(FolioCreateDto dto)
        {
            // Validación de negocio: no duplicados de año
            if (await _repository.ExistsByCodeAsync(dto.Numero))
                throw new InvalidOperationException($"Ya existe una Folio para el codigo {dto.Numero}.");

            var folio = new Folio
            {
                Numero = dto.Numero,
                ContratoId= dto.ContratoId,
                ArchivoPdf = dto.ArchivoPdf
            };

            var created = await _repository.AddAsync(folio);

            return new FolioDto { Id = created.Id, Numero = created.Numero, ContratoId = created.ContratoId, ArchivoPdf = dto.ArchivoPdf };
        }

        /// <summary>
        /// Obtiene todas las Folios
        /// </summary>
        public async Task<IEnumerable<FolioDto>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return list.Select(v => new FolioDto { Id = v.Id, Numero = v.Numero, ArchivoPdf = v.ArchivoPdf });
        }

        /// <summary>
        /// Obtiene por Id; retorna null si no existe
        /// </summary>
        public async Task<FolioDto?> GetByIdAsync(int id)
        {
            var v = await _repository.GetByIdAsync(id);
            if (v == null) return null;
            return new FolioDto { Id = v.Id, Numero = v.Numero };
        }

        /// <summary>
        /// Actualiza una Folio. Valida existencia y duplicidad de año.
        /// </summary>
        public async Task UpdateAsync(int id, FolioUpdateDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Folio con id {id} no encontrada.");

            // Validar duplicado en otro registro
            if (await _repository.ExistsByCodeAsync(dto.Numero, excludingId: id))
                throw new InvalidOperationException($"Ya existe otra Folio para el codigo {dto.Numero}.");

            // Mapear cambios
            existing.Numero = dto.Numero;
            existing.ArchivoPdf = dto.ArchivoPdf;

            await _repository.UpdateAsync(existing);
        }

        /// <summary>
        /// Elimina una Folio por id.
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Folio con id {id} no encontrada.");

            // Si necesitas validar relaciones (ej. anaqueles), hazlo aquí antes de borrar.

            await _repository.DeleteAsync(existing);
        }
    }
}
