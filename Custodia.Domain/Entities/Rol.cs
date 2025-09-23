﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custodia.Domain.Entities
{
    public class Rol
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Nombre { get; set; } = null!;

        // Relación 1:N con Usuarios
        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
