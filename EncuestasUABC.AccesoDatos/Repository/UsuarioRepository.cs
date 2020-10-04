using EncuestasUABC.AccesoDatos.Data;
using EncuestasUABC.AccesoDatos.Repository.Interfaces;
using EncuestasUABC.Constantes;
using EncuestasUABC.Models;
using EncuestasUABC.Models.Catalogos;
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

        public async Task<List<ApplicationUser>> GetAll()
        {
            #region GetAll

            return await _context.ApplicationUser
                .Include(x => x.RolIdNavigation)
                .Where(x => x.Activo).ToListAsync();

            #endregion
        }

        public async Task<List<ApplicationUser>> GetAll(string rol)
        {
            #region GetAll
            return await _context.ApplicationUser
                        .Include(x => x.RolIdNavigation)
                        .Where(x => x.Activo && x.RolIdNavigation.Descripcion.Equals(rol)).ToListAsync();
            #endregion
        }

        public async Task<ApplicationUser> GetById(string id)
        {
            #region Get

            return await _context.ApplicationUser
                 .Include(x => x.Alumno)
                 .Include(x => x.Egresado)
                 .Include(x => x.Administrativo)
                 .Include(x => x.RolIdNavigation)
                 .FirstOrDefaultAsync(x => x.Id.Equals(id));

            #endregion
        }

        public async Task<ApplicationUser> Get(string userName)
        {
            #region Get

            return await _context.ApplicationUser
                 .Include(x => x.Alumno).ThenInclude(x => x.CarreraIdNavigation).ThenInclude(x => x.UnidadAcademicaIdNavigation).ThenInclude(x => x.CampusIdNavigation)
                 .Include(x => x.Egresado)
                 .Include(x => x.Administrativo)
                 .Include(x => x.RolIdNavigation)
                  .Where(x => x.UserName.Equals(userName))
                  .FirstOrDefaultAsync();

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
        public async Task<List<Permiso>> PermisosUsuario(string userId)
        {
            #region PermisosUsuario
            return await _context.UsuariosPermisos
                .Include(x => x.PermisoIdNavigation)
                .Include(x => x.PermisoIdNavigation.PermisosHijos)
                .Where(x => x.UsuarioId.Equals(userId) && !x.PermisoIdNavigation.PermisoIdPadre.HasValue)
                .OrderBy(x => x.PermisoIdNavigation.Descripcion)
                .Select(x => x.PermisoIdNavigation).ToListAsync();
            #endregion
        }
        public async Task<List<Permiso>> AllPermisosUsuario()
        {
            #region AllPermisosUsuario
            return await _context.Permisos
                .Include(x => x.PermisosHijos)
                .ThenInclude(x=>x.PermisosHijos)
                .Where(x => !x.PermisoIdPadre.HasValue)
                .OrderBy(x => x.PermisoIdPadreNavigation.Descripcion)
                .ToListAsync();
            #endregion
        }
    }
}
