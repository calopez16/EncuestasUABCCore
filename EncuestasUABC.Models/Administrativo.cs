using System;

namespace EncuestasUABC.Models
{
    public class Administrativo
    {
        public Administrativo()
        {
            FechaRegistro = DateTime.Now;
        }
        public int Id { get; set; }
        public DateTime FechaRegistro{ get; set; }
        public string NumeroEmpleado { get; set; }
        public string Correo { get; set; }
        public int IdTipoAdministrativo { get; set; }
        public bool? Estatus { get; set; }
        public TipoAdministrativo IdTipoAdministrativoNavigation { get; set; }
        public virtual Academico AcademicoNavigation { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
