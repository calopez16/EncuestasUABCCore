using EncuestasUABC.AccesoDatos.Data;
using EncuestasUABC.Constantes;
using EncuestasUABC.Enumerador;
using EncuestasUABC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EncuestasUABC.AccesoDatos.Inicializador
{
    public class Inicializador : IInicializador
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public Inicializador(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void Inicializar()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception)
            {
            }

            if (!_context.Roles.Any(x => x.Name.Equals(RolesSistema.Administrador)))
            {
                _roleManager.CreateAsync(new IdentityRole(RolesSistema.Administrador)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(RolesSistema.Usuario)).GetAwaiter().GetResult();
            }
            if (!_context.ApplicationUser.Any(x => x.UserName.Equals(Defaults.AdminEmail)))
            {
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = Defaults.AdminEmail,
                    Email = Defaults.AdminEmail,
                    EmailConfirmed = true,
                    Nombre = "Admin",
                    ApellidoPaterno = "Admin",
                    ApellidoMaterno = "Admin",
                    Estatus = true
                }, Defaults.AdminPass).GetAwaiter().GetResult();

                ApplicationUser usuario = _context.ApplicationUser
                    .FirstOrDefault(x => x.Email.Equals(Defaults.AdminEmail));

                _userManager.AddToRoleAsync(usuario, RolesSistema.Administrador).GetAwaiter().GetResult();
            }

        }
    }
}
