using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodia.Domain.Entities
{
    public class Vigencia
    {
        public int Id { get; set; }
        public int Anio { get; set; }

        public virtual ICollection<Anaquel> Anaqueles { get; set; } = new List<Anaquel>();
    }
}
