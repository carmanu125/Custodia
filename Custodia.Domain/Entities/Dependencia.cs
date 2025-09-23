using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodia.Domain.Entities
{
    public class Dependencia
    {
        public int Id { get; set; }

        [StringLength(150)]
        public string? Nombre { get; set; }

        public int CajaId { get; set; }
        public virtual Caja Caja { get; set; } = null!;
        public string? Observaciones { get; set; }

        public virtual ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
    }
}
