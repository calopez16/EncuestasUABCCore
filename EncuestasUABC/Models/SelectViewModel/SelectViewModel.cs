using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EncuestasUABC.Models.SelectViewModel
{
    public class SelectViewModel
    {
        public string search { get; set; }
        public int page { get; set; }
        public int perPage { get; set; }
    }
}
