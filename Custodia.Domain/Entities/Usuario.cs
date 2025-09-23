using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodia.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Nombre { get; set; } = null!;

        [Required, StringLength(100)]
        public string Correo { get; set; } = null!;

        [Required]
        public string Contrasena { get; set; } = null!;

        public int RolId { get; set; }
        public virtual Rol Rol { get; set; } = null!;

        // Relación con Trazabilidad
        public virtual ICollection<Trazabilidad> Trazas { get; set; } = new List<Trazabilidad>();
    }
}
