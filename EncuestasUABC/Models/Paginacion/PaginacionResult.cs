using System.Collections.Generic;

namespace EncuestasUABC.Models.Paginacion
{
    public class PaginacionResult<T>
    {
        public List<T> Resultado { get; set; }
        public int TotalRegistros { get; set; }
    }
}
