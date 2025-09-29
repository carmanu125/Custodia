using Custodia.Application.Common.Interfaces;
using Custodia.Application.DTOs;
using Custodia.Application.DTOs.Caja;
using Custodia.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Custodia.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CajasController : ControllerBase
    {
        private readonly CajaService _service;

        public CajasController(CajaService service)
        {
            _service = service;
        }

        /// <summary>
        /// POST: api/Cajas
        /// Crea una nueva Caja
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCajaDto dto)
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
        /// GET: api/Cajas
        /// Lista todas las Cajas
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        /// <summary>
        /// GET: api/Cajas/{id}
        /// Obtiene Caja por id
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var v = await _service.GetByIdAsync(id);
            if (v == null) return NotFound();
            return Ok(v);
        }

        /// <summary>
        /// PUT: api/Cajas/{id}
        /// Actualiza la Caja indicada
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CajaUpdateDto dto)
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
        /// DELETE: api/Cajas/{id}
        /// Elimina Caja por id
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
