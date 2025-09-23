using Custodia.Application.DTOs;
using Custodia.Application.Services;
using Custodia.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Custodia.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContratosController : ControllerBase
    {
        private readonly ContratoService _contratoService;

        public ContratosController(ContratoService contratoService)
        {
            _contratoService = contratoService;
        }

        [HttpPost]
        public async Task<ActionResult<Contrato>> CrearContrato([FromBody] ContratoCreateDto contratoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var contrato = await _contratoService.CrearContratoAsync(contratoDto);

            return CreatedAtAction(nameof(GetById), new { id = contrato.Id }, contrato);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // este es solo un ejemplo (puedes implementarlo en el servicio/repositorio)
            return Ok(new { Id = id, Message = "Aquí devolverías el contrato" });
        }
    }
}
