using EncuestasUABC.AccesoDatos.Data;
using EncuestasUABC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using EncuestasUABC.Constantes;
using EncuestasUABC.Models.Relaciones;

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

            if (_context.Roles.Any(x => x.Name.Equals(RolesSistema.ADMINISTRADOR))) return;
            _roleManager.CreateAsync(new IdentityRole(RolesSistema.ADMINISTRADOR)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(RolesSistema.MAESTRO)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(RolesSistema.COORDINADOR)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(RolesSistema.TUTOR)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(RolesSistema.ALUMNO)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(RolesSistema.EGRESADO)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "admin@uabc.edu.mx",
                Email = "admin@uabc.edu.mx",
                EmailConfirmed = true,
                Nombre = "Admin",
                ApellidoPaterno="Admin",
                ApellidoMaterno="Admin",
                Activo =true
            }, "Admin123!").GetAwaiter().GetResult();

            ApplicationUser usuario = _context.ApplicationUser
                .Where(x => x.Email.Equals("admin@uabc.edu.mx"))
                .FirstOrDefault();
            _userManager.AddToRoleAsync(usuario, RolesSistema.ADMINISTRADOR).GetAwaiter().GetResult();
            var permisos =new  List<UsuarioPermiso>();
            foreach (var permiso in _context.Permisos)
            {
                permisos.Add(new UsuarioPermiso { Permiso = permiso });
            }
            usuario.Permisos = permisos;
            _context.Entry(usuario).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
