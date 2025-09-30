using Custodia.Application.DTOs;
using Custodia.Application.DTOs.Folio;
using Custodia.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Custodia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FolioController : ControllerBase
    {
        private readonly FolioService _service;

        public FolioController(FolioService service)
        {
            _service = service;
        }

        /// <summary>
        /// POST: api/Folios
        /// Crea una nueva Folio
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FolioCreateDto dto)
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
        /// GET: api/Folios
        /// Lista todas las Folios
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        /// <summary>
        /// GET: api/Folios/{id}
        /// Obtiene Folio por id
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var v = await _service.GetByIdAsync(id);
            if (v == null) return NotFound();
            return Ok(v);
        }

        /// <summary>
        /// PUT: api/Folios/{id}
        /// Actualiza la Folio indicada
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] FolioUpdateDto dto)
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
        /// DELETE: api/Folios/{id}
        /// Elimina Folio por id
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
