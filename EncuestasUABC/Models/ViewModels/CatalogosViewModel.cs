namespace EncuestasUABC.Models.ViewModels.Catalogos
{
    public class CampusViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Estatus { get; set; }
    }
    public class CarreraViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdUnidadAcademica { get; set; }
        public bool Estatus { get; set; }
        public UnidadAcademicaViewModel IdUnidadAcademicaNavigation { get; set; }
    }

    public class UnidadAcademicaViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdCampus { get; set; }
        public bool Estatus { get; set; }
        public CampusViewModel IdCampusNavigation { get; set; }

    }

}
