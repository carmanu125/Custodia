using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodia.Domain.Entities
{
    public class Anaquel
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Nombre { get; set; } = null!;

        public int VigenciaId { get; set; }
        public virtual Vigencia Vigencia { get; set; } = null!;

        public virtual ICollection<Caja> Cajas { get; set; } = new List<Caja>();
    }
}
