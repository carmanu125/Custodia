using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodia.Application.DTOs
{
    public class AnaquelCreateDto
    {
        public string Nombre { get; set; } = null!;
        public int VigenciaId { get; set; }
    }
}
