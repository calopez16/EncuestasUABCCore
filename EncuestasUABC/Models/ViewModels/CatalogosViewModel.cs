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
        public int UnidadAcademicaId { get; set; }
        public bool Estatus { get; set; }
        public UnidadAcademicaViewModel UnidadAcademicaIdNavigation { get; set; }
    }

    public class UnidadAcademicaViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int CampusId { get; set; }
        public bool Estatus { get; set; }
        public CampusViewModel CampusIdNavigation { get; set; }

    }

}
