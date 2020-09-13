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

            return await _context.ApplicationUser.Where(x => x.Activo).ToListAsync();

            #endregion
        }

        public async Task<List<ApplicationUser>> GetAll(string rol)
        {
            #region GetAll
            var usuarios = await _context.ApplicationUser.Where(x => x.Activo).ToListAsync();
            var usuariosResult = new List<ApplicationUser>();
            usuariosResult.AddRange(usuarios);
            foreach (var usuario in usuarios)
            {
                if (!await _userManager.IsInRoleAsync(usuario, rol))
                {
                    usuariosResult.Remove(usuario);
                }
            }
            return usuariosResult;
            #endregion
        }

        public async Task<ApplicationUser> GetById(string id)
        {
            #region Get

            var user = await _context.ApplicationUser
                 .Include(x => x.UsuarioAlumno).ThenInclude(x => x.Alumno)
                 .Include(x => x.UsuarioEgresado).ThenInclude(x => x.Egresado)
                 .Include(x => x.UsuarioMaestro).ThenInclude(x => x.Maestro)
                  .FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (user != null)
            {
                user.Rol = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            }
            return user;


            #endregion
        }

        public async Task<ApplicationUser> Get(string userName)
        {
            #region Get

            var user = await _context.ApplicationUser
                 .Include(x => x.UsuarioAlumno).ThenInclude(x => x.Alumno).ThenInclude(x => x.Carrera).ThenInclude(x => x.UnidadAcademica).ThenInclude(x => x.Campus)
                 .Include(x => x.UsuarioEgresado).ThenInclude(x => x.Egresado)
                 .Include(x => x.UsuarioMaestro).ThenInclude(x => x.Maestro)
                  .Where(x => x.UserName.Equals(userName))
                  .FirstOrDefaultAsync();
            if (user != null)
            {
                user.Rol = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            }
            return user;


            #endregion
        }

        public async Task<IdentityResult> Create(ApplicationUser user)
        {
            #region Create

            return await _userManager.CreateAsync(user, Defaults.CONTRASENA);

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

            var resetToken=await _userManager.GeneratePasswordResetTokenAsync(user);
            return await _userManager.ResetPasswordAsync(user, resetToken, nuevaContrasena);

            #endregion
        }
        public async Task<IdentityResult> CambiarContrasena(ApplicationUser user,string contrasenaActual, string nuevaContrasena)
        {
            #region CambiarContrasena

            return await _userManager.ChangePasswordAsync(user, contrasenaActual, nuevaContrasena);

            #endregion
        }
        public async Task<List<Permiso>> PermisosUsuario(string userId)
        {
            #region PermisosUsuario
            var permisos = await _context.UsuariosPermisos
                .Include(x => x.Permiso)
                .Include(x => x.Permiso.PermisosHijos)
                .Where(x => x.UsuarioId.Equals(userId) && !x.Permiso.PermisoIdPadre.HasValue)
                .OrderBy(x => x.Permiso.Descripcion)
                .Select(x => x.Permiso).ToListAsync();
            return permisos;
            #endregion
        }


    }
}
