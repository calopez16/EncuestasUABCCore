using System;
using System.Collections.Generic;
using System.Text;

namespace EncuestasUABC.Models
{
    public class Maestro
    {
        public int Id { get; set; }
        public string NumeroEmpleado { get; set; }
        public string Correo { get; set; }
        public string CorreoAlterno { get; set; }
        public string Telefono { get; set; }

    }
}
