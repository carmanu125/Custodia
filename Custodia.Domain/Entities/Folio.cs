using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodia.Domain.Entities
{
    public class Folio
    {
        public int Id { get; set; }
        public int Numero { get; set; }

        [Required]
        public string ArchivoPdf { get; set; } = null!;

        public int ContratoId { get; set; }
        public virtual Contrato? Contrato { get; set; } = null!;

        public virtual ICollection<Trazabilidad> Trazas { get; set; } = new List<Trazabilidad>();
    }
}
