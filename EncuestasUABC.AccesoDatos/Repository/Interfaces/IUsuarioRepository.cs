using EncuestasUABC.Models;
using EncuestasUABC.Models.Catalogos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EncuestasUABC.AccesoDatos.Repository.Interfaces
{
    public interface IUsuarioRepository:IDisposable
    {
        Task<List<Permiso>> PermisosUsuario(string userId);
        /// <summary>
        /// Obtiene a todos los usuarios.
        /// </summary>
        /// <returns></returns>
        Task<List<ApplicationUser>> GetAll();
        /// <summary>
        /// Obtiene los registros de usuarios por Rol.
        /// </summary>
        /// <param name="rol">Rol por el cual se va a filtrar.</param>
        /// <returns></returns>
        Task<List<ApplicationUser>> GetAll(string rol);
        Task<ApplicationUser> Get(string userName);
        Task<ApplicationUser> GetById(string id);
        Task<IdentityResult> Create(ApplicationUser user);
        Task<IdentityResult> Update(ApplicationUser user);
        Task<IdentityResult> CambiarContrasena(ApplicationUser user, string nuevaContrasena);
        Task<IdentityResult> CambiarContrasena(ApplicationUser user, string contrasenaActual, string nuevaContrasena);
        Task<List<Permiso>> AllPermisos();
        Task<List<Permiso>> AllPermisosByUser(string userId);
    }
}
