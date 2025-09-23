using Custodia.Application.Common.Interfaces;
using Custodia.Application.DTOs;
using Custodia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodia.Application.Services
{
    public class ContratoService
    {
        private readonly IContratoRepository _contratoRepository;

        public ContratoService(IContratoRepository contratoRepository)
        {
            _contratoRepository = contratoRepository;
        }

        public async Task<Contrato> CrearContratoAsync(ContratoCreateDto dto)
        {
            // Mapear DTO a Entity
            var contrato = new Contrato
            {
                Numero = dto.Numero,
                Descripcion = dto.Descripcion,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin,
                DependenciaId = dto.DependenciaId
            };

            await _contratoRepository.AddAsync(contrato);
            return contrato;
        }
    }
}
