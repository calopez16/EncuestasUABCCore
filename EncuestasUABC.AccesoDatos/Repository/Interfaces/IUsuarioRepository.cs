using EncuestasUABC.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EncuestasUABC.AccesoDatos.Repository.Interfaces
{
    public interface IUsuarioRepository:IDisposable
    {
        Task<List<ApplicationUser>> GetAll(string rol = null, bool activo = true);
        Task<List<Permiso>> PermisosByUser(string userId);
        Task<List<Permiso>> Permisos();
        Task<IdentityResult> Create(ApplicationUser user);
        Task<IdentityResult> Update(ApplicationUser user);
        Task<string> GetRolByUser(ApplicationUser user);
        Task<IdentityResult> SetRolToUser(ApplicationUser user, string rol);
        Task<IdentityResult> RemoveRolOfUser(ApplicationUser user, string rol);
        Task<IdentityResult> CambiarContrasena(ApplicationUser user, string nuevaContrasena);
        Task<IdentityResult> CambiarContrasena(ApplicationUser user, string contrasenaActual, string nuevaContrasena);
    }
}
