using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodia.Domain.Entities
{
    public class Trazabilidad
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; } = null!;

        [Required, StringLength(50)]
        public string Accion { get; set; } = null!; // CREAR, ACTUALIZAR, ELIMINAR...

        public int? ContratoId { get; set; }
        public virtual Contrato? Contrato { get; set; }

        public int? FolioId { get; set; }
        public virtual Folio? Folio { get; set; }

        public string? Detalles { get; set; }

        public DateTime Fecha { get; set; } = DateTime.UtcNow;
    }
}
