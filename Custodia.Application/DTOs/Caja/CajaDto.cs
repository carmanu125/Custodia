using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodia.Application.DTOs.Caja
{
    public class CajaDto
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public int AnaquelId { get; set; }
    }
}
