using Custodia.Application.DTOs;
using Custodia.Application.DTOs.Usuario;
using Custodia.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Custodia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _service;

        public UsuariosController(UsuarioService service)
        {
            _service = service;
        }

        /// <summary>
        /// POST: api/Usuarios
        /// Crea una nueva Usuario
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UsuarioCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (InvalidOperationException ex)
            {
                // 409 Conflict en caso de regla de negocio (ej: duplicado)
                return Conflict(new { error = ex.Message });
            }
        }

        /// <summary>
        /// GET: api/Usuarios
        /// Lista todas las Usuarios
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        /// <summary>
        /// GET: api/Usuarios/{id}
        /// Obtiene Usuario por id
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var v = await _service.GetByIdAsync(id);
            if (v == null) return NotFound();
            return Ok(v);
        }

        /// <summary>
        /// PUT: api/Usuarios/{id}
        /// Actualiza la Usuario indicada
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UsuarioUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _service.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { error = ex.Message });
            }
        }

        /// <summary>
        /// DELETE: api/Usuarios/{id}
        /// Elimina Usuario por id
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
