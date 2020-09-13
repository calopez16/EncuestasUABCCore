using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EncuestasUABC.Models
{
    public class PaginacionViewModel<T> where T : class
    {
        public T Result { get; set; }
        public int NumeroPagina { get; set; }
        public int TotalRegistros { get; set; }
        public int TotalPaginas { get; set; }
    }
}
