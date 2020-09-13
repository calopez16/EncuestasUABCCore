using EncuestasUABC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EncuestasUABC.AccesoDatos.Repository.Interfaces
{
    public interface IEncuestasRepository : IDisposable
    {
        Task<IEnumerable<Encuesta>> GetAll();
        Task<IEnumerable<Encuesta>> GetByUser(string userId);
        Task<Encuesta> Add(Encuesta encuesta);
        Task<Encuesta> Get(int id);
        Task<int> Update(Encuesta encuesta);
    }
}
