using Custodia.Application.Common.Interfaces;
using Custodia.Application.DTOs.Anaquel;
using Custodia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodia.Application.Services
{
    public class AnaquelService
    {
        private readonly IAnaquelRepository _repository;
        private readonly IVigenciaRepository _vigenciaRepository;

        public AnaquelService(IAnaquelRepository repository, IVigenciaRepository vigenciaRepository)
        {
            _repository = repository;
            _vigenciaRepository = vigenciaRepository;
        }

        public async Task<AnaquelDto> CreateAsync(AnaquelCreateDto dto)
        {
            // Validar que exista la Vigencia
            var vigencia = await _vigenciaRepository.GetByIdAsync(dto.VigenciaId);
            if (vigencia == null)
                throw new KeyNotFoundException($"La vigencia con id {dto.VigenciaId} no existe.");

            // Validar duplicado de código por vigencia
            if (await _repository.ExistsByNombreAsync(dto.Nombre, dto.VigenciaId))
                throw new InvalidOperationException($"El código {dto.Nombre} ya existe en la vigencia {dto.VigenciaId}.");

            var anaquel = new Anaquel
            {
                Nombre = dto.Nombre,
                VigenciaId = dto.VigenciaId
            };

            var created = await _repository.AddAsync(anaquel);
            return new AnaquelDto { Id = created.Id, Nombre = created.Nombre, VigenciaId = created.VigenciaId };
        }

        public async Task<IEnumerable<AnaquelDto>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return list.Select(a => new AnaquelDto
            {
                Id = a.Id,
                Nombre = a.Nombre,
                VigenciaId = a.VigenciaId
            });
        }

        public async Task<AnaquelDto?> GetByIdAsync(int id)
        {
            var a = await _repository.GetByIdAsync(id);
            if (a == null) return null;

            return new AnaquelDto { Id = a.Id, Nombre = a.Nombre, VigenciaId = a.VigenciaId };
        }

        public async Task UpdateAsync(int id, AnaquelUpdateDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Anaquel con id {id} no encontrado.");

            if (await _repository.ExistsByNombreAsync(dto.Nombre, dto.VigenciaId, excludingId: id))
                throw new InvalidOperationException($"El código {dto.Nombre} ya existe en la vigencia {dto.VigenciaId}.");

            existing.Nombre = dto.Nombre;
            existing.VigenciaId = dto.VigenciaId;

            await _repository.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Anaquel con id {id} no encontrado.");

            await _repository.DeleteAsync(existing);
        }
    }
}
