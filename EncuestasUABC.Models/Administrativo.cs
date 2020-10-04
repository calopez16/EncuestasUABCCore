using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EncuestasUABC.Models
{
    public class Administrativo
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public string NumeroEmpleado { get; set; }
        public string Correo { get; set; }
        public string CorreoAlterno { get; set; }
        public string Telefono { get; set; }
        public ApplicationUser UsuarioIdNavigation { get; set; }
    }
}
