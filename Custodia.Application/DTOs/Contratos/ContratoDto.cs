using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodia.Application.DTOs.Contratos
{
    public class ContratoDto
    {
        public int Id { get; set; }
        public string Numero { get; set; } = null!;
        public string? Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        public int DependenciaId { get; set; }
    }
}
