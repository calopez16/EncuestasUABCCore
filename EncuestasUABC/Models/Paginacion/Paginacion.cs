
namespace EncuestasUABC.Models.Paginacion
{
    public class Paginacion
    {
        /// <summary>
        /// Contador de página.
        /// Esto es utilizado por DataTables para garantizar que los retornos de Ajax de las solicitudes de procesamiento del lado del servidor se dibujan en secuencia por DataTables (las solicitudes de Ajax son asíncronas y, por lo tanto, pueden volver a salir de la secuencia).
        /// Esto se usa como parte del parámetro de retorno de sorteo
        /// </summary>
        public int Draw { get; set; }

        /// <summary>
        /// Paging first record indicator.
        /// Sirve para indicar en que numero de index comenzara la paginación
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// Indica el número de registros que se mostrarán en la tabla.
        /// En caso de que el valor sea -1 quiere decir que el total de registros es menor que la cantidad que se requiere mostrar en la paginacion.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Contiene los valores de busqueda en caso de que se haya ingresado un texto en el input de busqueda.
        /// </summary>
        public PaginacionBusqueda Search { get; set; }

        public PaginacionOrden[] Order { get; set; }
        /// <summary>
        /// Filtros extras que se requieran buscar, esto ya es a convenir del programador o del metodo que requiere usar.
        /// </summary>
        public object[] OtrosFiltros { get; set; }

    }
}
