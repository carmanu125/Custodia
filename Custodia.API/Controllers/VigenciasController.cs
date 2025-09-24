using Custodia.Application.DTOs;
using Custodia.Application.DTOs.Vigencia;
using Custodia.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Custodia.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VigenciasController : ControllerBase
    {
        private readonly VigenciaService _service;

        public VigenciasController(VigenciaService service)
        {
            _service = service;
        }

        /// <summary>
        /// POST: api/vigencias
        /// Crea una nueva vigencia
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VigenciaCreateDto dto)
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
        /// GET: api/vigencias
        /// Lista todas las vigencias
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        /// <summary>
        /// GET: api/vigencias/{id}
        /// Obtiene vigencia por id
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var v = await _service.GetByIdAsync(id);
            if (v == null) return NotFound();
            return Ok(v);
        }

        /// <summary>
        /// PUT: api/vigencias/{id}
        /// Actualiza la vigencia indicada
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] VigenciaUpdateDto dto)
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
        /// DELETE: api/vigencias/{id}
        /// Elimina vigencia por id
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
