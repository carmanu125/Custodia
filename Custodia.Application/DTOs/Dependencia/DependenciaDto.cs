using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodia.Application.DTOs.Dependencia
{
    public class DependenciaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Observaciones { get; set; }
        public int CajaId { get; set; }
    }
}
