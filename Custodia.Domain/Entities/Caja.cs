using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodia.Domain.Entities
{
    public class Caja
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Codigo { get; set; } = null!;

        public int AnaquelId { get; set; }
        public virtual Anaquel Anaquel { get; set; } = null!;

        public virtual ICollection<Dependencia> Dependencias { get; set; } = new List<Dependencia>();
    }
}
