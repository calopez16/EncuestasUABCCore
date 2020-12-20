using EncuestasUABC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EncuestasUABC.AccesoDatos.Repository.Interfaces
{
    public interface IEncuestasRepository : IDisposable
    {
        Task<EncuestaSeccion> GetEncuestaSeccionById(int id, int idEncuesta);
        Task<Encuesta> GetById(int id);
        Task<EncuestaSeccion> GetSeccionById(int id, int encuestaId);
        Task<EncuestaPregunta> GetPreguntaById(int id, int encuestaId, int seccionId);
        Task<EncuestaPregunta> GetPreguntaByIdDetalle(int id, int encuestaId, int seccionId);
        Task<EncuestaSeccion> GetPrimeraSeccionById(int encuestaId);
        Task<List<EncuestaSeccion>> GetSecciones(int encuestaId);
    }
}
