using EncuestasUABC.AccesoDatos.Data;
using EncuestasUABC.AccesoDatos.Repository.Interfaces;
using EncuestasUABC.Constantes;
using EncuestasUABC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EncuestasUABC.AccesoDatos.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        protected readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public UsuarioRepository(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }
        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
            if (_userManager != null)
                _userManager.Dispose();
        }
        public async Task<List<ApplicationUser>> GetAll(string rol = null, bool activo = true, string nombre = null, string correo = null)
        {
            #region GetAll
            var usuarios = (from user in _context.ApplicationUser
                            join userRoles in _context.UserRoles on user.Id equals userRoles.UserId
                            join role in _context.Roles on userRoles.RoleId equals role.Id
                            where user.Activo == activo && !user.Email.Equals(Defaults.AdminEmail)
                            select new ApplicationUser
                            {
                                Id = user.Id,
                                Email = user.Email,
                                Nombre = user.Nombre,
                                ApellidoPaterno = user.ApellidoPaterno,
                                ApellidoMaterno = user.ApellidoMaterno,
                                UserName = user.UserName,
                                Rol = role.Name,
                                Activo=user.Activo
                            }).AsQueryable();

            if (!string.IsNullOrEmpty(correo))
                usuarios = usuarios.Where(x => x.Email.Equals(correo));

            if (!string.IsNullOrEmpty(nombre))
                usuarios = usuarios.Where(x => x.Nombre.Equals(nombre)
                || x.ApellidoPaterno.Equals(nombre)
                || x.ApellidoMaterno.Equals(nombre));

            if (!string.IsNullOrEmpty(rol))
                usuarios = usuarios.Where(x => x.Rol.Equals(rol));
            return await usuarios.ToListAsync();

            #endregion
        }

        public async Task<IdentityResult> Create(ApplicationUser user)
        {
            #region Create

            return await _userManager.CreateAsync(user, Defaults.Contrasena);

            #endregion
        }

        public async Task<string> GetRolByUser(ApplicationUser user)
        {
            #region GetRolByUser

            return (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            #endregion
        }

        public async Task<IdentityResult> SetRolToUser(ApplicationUser user, string rol)
        {
            #region SetRolToUser

            return await _userManager.AddToRoleAsync(user, rol);

            #endregion
        }

        public async Task<IdentityResult> RemoveRolOfUser(ApplicationUser user, string rol)
        {
            #region RemoveRolOfUser

            return await _userManager.RemoveFromRoleAsync(user, rol);

            #endregion
        }

        public async Task<IdentityResult> Update(ApplicationUser user)
        {
            #region Edit

            return await _userManager.UpdateAsync(user);

            #endregion
        }

        public async Task<IdentityResult> CambiarContrasena(ApplicationUser user, string nuevaContrasena)
        {
            #region CambiarContrasena

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            return await _userManager.ResetPasswordAsync(user, resetToken, nuevaContrasena);

            #endregion
        }
        public async Task<IdentityResult> CambiarContrasena(ApplicationUser user, string contrasenaActual, string nuevaContrasena)
        {
            #region CambiarContrasena

            return await _userManager.ChangePasswordAsync(user, contrasenaActual, nuevaContrasena);

            #endregion
        }

        public async Task<List<Permiso>> PermisosByUser(string userId)
        {
            #region PermisosAll
            var permisos = await _context.UsuariosPermisos
                .Include(x => x.PermisoIdNavigation)
                .Where(x => x.UsuarioId.Equals(userId))
                .OrderBy(x => x.PermisoIdNavigation.Descripcion)
                .Select(x => x.PermisoIdNavigation)
                .ToListAsync();
            return permisos;
            #endregion
        }

        public async Task<List<Permiso>> Permisos()
        {
            #region Permisos
            return await _context.Permisos
                .Include(x => x.PermisosHijos)
                .ThenInclude(x => x.PermisosHijos)
                .ThenInclude(x => x.PermisosHijos)
                .Where(x => x.Menu && !x.PermisoIdPadre.HasValue).ToListAsync();
            #endregion
        }
    }
}
