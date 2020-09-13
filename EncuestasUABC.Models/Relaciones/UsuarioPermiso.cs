﻿using EncuestasUABC.Models.Catalogos;
using System.ComponentModel.DataAnnotations.Schema;

namespace EncuestasUABC.Models.Relaciones
{
    public class UsuarioPermiso
    {
        public string UsuarioId { get; set; }
        public int PermisoId { get; set; }
        [ForeignKey(nameof(UsuarioId))]
        public ApplicationUser Usuario { get; set; }
        [ForeignKey(nameof(PermisoId))]
        public Permiso Permiso { get; set; }


    }
}