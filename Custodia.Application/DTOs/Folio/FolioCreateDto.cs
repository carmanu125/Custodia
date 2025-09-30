using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodia.Application.DTOs.Folio
{
    public class FolioCreateDto
    {
        public int Numero { get; set; }
        public string ArchivoPdf { get; set; } = null!;
        public int ContratoId { get; set; }
    }
}
