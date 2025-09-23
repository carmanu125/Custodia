using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodia.Application.DTOs
{
    public class CajaCreateDto
    {
        public string Codigo { get; set; } = null!;
        public int AnaquelId { get; set; }
    }
}
