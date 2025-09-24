using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodia.Application.DTOs.Anaquel
{
    public class AnaquelDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int VigenciaId { get; set; }
    }
}
