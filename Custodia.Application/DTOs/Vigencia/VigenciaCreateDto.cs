using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodia.Application.DTOs
{
    public class VigenciaCreateDto
    {
        [Required]
        [Range(1900, 3000)]
        public int Anio { get; set; }
    }
}
