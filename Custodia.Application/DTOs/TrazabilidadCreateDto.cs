using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodia.Application.DTOs
{
    public class TrazabilidadCreateDto
    {
        public int UsuarioId { get; set; }
        public string Accion { get; set; } = null!;
        public int? ContratoId { get; set; }
        public int? FolioId { get; set; }
        public string? Detalles { get; set; }
    }
}
