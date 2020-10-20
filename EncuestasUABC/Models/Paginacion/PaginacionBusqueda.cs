
namespace EncuestasUABC.Models.Paginacion
{
    public class PaginacionBusqueda
    {
        /// <summary>
        /// Valor que se requiere buscar.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// true if the global filter should be treated as a regular expression for advanced searching, false otherwise.
        /// Note that normally server-side processing scripts will not perform regular expression searching for performance reasons on large data sets, but it is technically possible and at the discretion of your script.
        /// </summary>
        public bool Regex { get; set; }
    }
}
