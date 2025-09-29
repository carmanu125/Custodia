using Custodia.Application.DTOs.Dependencia;
using Custodia.Application.DTOs;
using Custodia.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Custodia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DependenciaController : ControllerBase
    {

        private readonly DependenciaService _service;

        public DependenciaController(DependenciaService service)
        {
            _service = service;
        }

        /// <summary>
        /// POST: api/Dependencias
        /// Crea una nueva Dependencia
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DependenciaCreateDto dto)
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
        /// GET: api/Dependencias
        /// Lista todas las Dependencias
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        /// <summary>
        /// GET: api/Dependencias/{id}
        /// Obtiene Dependencia por id
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var v = await _service.GetByIdAsync(id);
            if (v == null) return NotFound();
            return Ok(v);
        }

        /// <summary>
        /// PUT: api/Dependencias/{id}
        /// Actualiza la Dependencia indicada
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] DependenciaUpdateDto dto)
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
        /// DELETE: api/Dependencias/{id}
        /// Elimina Dependencia por id
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
