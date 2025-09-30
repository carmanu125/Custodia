using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodia.Application.DTOs.Usuario
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Contrasena { get; set; } = null!;
        public int RolId { get; set; }
    }
}
