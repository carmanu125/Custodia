using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodia.Domain.Entities
{
    public class Contrato
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Numero { get; set; } = null!;

        public string? Descripcion { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaInicio { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaFin { get; set; }

        public int DependenciaId { get; set; }
        public virtual Dependencia? Dependencia { get; set; } = null!;

        public virtual ICollection<Folio> Folios { get; set; } = new List<Folio>();

        // Relación con trazabilidad
        public virtual ICollection<Trazabilidad> Trazas { get; set; } = new List<Trazabilidad>();
    }
}
