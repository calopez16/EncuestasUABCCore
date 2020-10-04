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

            if (_context.ApplicationUser.Any(x => x.Email.Equals(Defaults.AdminEmail))) return;        

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = Defaults.AdminEmail,
                Email = Defaults.AdminEmail,
                EmailConfirmed = true,
                Nombre = "Admin",
                ApellidoPaterno="Admin",
                ApellidoMaterno="Admin",
                Activo =true,
                RolId=(int)RolId.Administrador

            }, "Admin123!").GetAwaiter().GetResult();
          
        }
    }
}
