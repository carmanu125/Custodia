using Custodia.Application.Common.Interfaces;
using Custodia.Application.DTOs;
using Custodia.Application.DTOs.Usuario;
using Custodia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodia.Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Crea una nueva Usuario después de validar unicidad del anio.
        /// Devuelve el DTO con Id generado.
        /// </summary>
        public async Task<UsuarioDto> CreateAsync(UsuarioCreateDto dto)
        {
            // Validación de negocio: no duplicados de año
            if (await _repository.ExistsByCorreoAsync(dto.Correo))
                throw new InvalidOperationException($"Ya existe una Usuario para el correo {dto.Correo}.");

            var usuario = new Usuario
            {
                Correo = dto.Correo,
                Nombre = dto.Nombre,
                Contrasena = dto.Contrasena,
                RolId = dto.RolId,  
            };

            var created = await _repository.AddAsync(usuario);

            return new UsuarioDto { Id = created.Id, Correo = created.Correo, Nombre = created.Nombre};
        }

        /// <summary>
        /// Obtiene todas las Usuarios
        /// </summary>
        public async Task<IEnumerable<UsuarioDto>> GetAllAsync()
        {
            var list = await _repository.GetAllAsync();
            return list.Select(v => new UsuarioDto { Id = v.Id, Correo = v.Correo, Nombre=v.Nombre});
        }

        /// <summary>
        /// Obtiene por Id; retorna null si no existe
        /// </summary>
        public async Task<UsuarioDto?> GetByIdAsync(int id)
        {
            var v = await _repository.GetByIdAsync(id);
            if (v == null) return null;
            return new UsuarioDto { Id = v.Id, Correo = v.Correo};
        }

        /// <summary>
        /// Actualiza una Usuario. Valida existencia y duplicidad de año.
        /// </summary>
        public async Task UpdateAsync(int id, UsuarioUpdateDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Usuario con id {id} no encontrada.");

            // Validar duplicado en otro registro
            if (await _repository.ExistsByCorreoAsync(dto.Correo, excludingId: id))
                throw new InvalidOperationException($"Ya existe otra Usuario para el correo {dto.Correo}.");

            // Mapear cambios
            existing.Correo = dto.Correo;
            existing.Nombre = dto.Nombre;
            existing.RolId = dto.RolId;

            await _repository.UpdateAsync(existing);
        }

        /// <summary>
        /// Elimina una Usuario por id.
        /// </summary>
        public async Task DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Usuario con id {id} no encontrada.");

            // Si necesitas validar relaciones (ej. anaqueles), hazlo aquí antes de borrar.

            await _repository.DeleteAsync(existing);
        }
    }
}

