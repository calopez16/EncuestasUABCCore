using EncuestasUABC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EncuestasUABC.AccesoDatos.Repository.Interfaces
{
    public interface IEncuestasRepository : IDisposable
    {
        Task<EncuestaSeccion> GetEncuestaSeccionById(int id, int idEncuesta);
        Task<Encuesta> GetById(int id);
    }
}
