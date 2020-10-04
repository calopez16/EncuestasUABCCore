using AutoMapper;
using EncuestasUABC.Models;
using EncuestasUABC.Models.Catalogos;
using EncuestasUABC.Models.ViewModels.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EncuestasUABC
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserViewModel>();
            CreateMap<ApplicationUserViewModel, ApplicationUser>();

            CreateMap<Alumno, AlumnoViewModel>();
            CreateMap<AlumnoViewModel, Alumno>();

            CreateMap<Administrativo, AdministrativoViewModel>();
            CreateMap<AdministrativoViewModel, Administrativo>();

            CreateMap<Egresado, EgresadoViewModel>();
            CreateMap<EgresadoViewModel, Egresado>();

            CreateMap<Rol, RolViewModel>();
            CreateMap<RolViewModel, Rol>();

            CreateMap<Permiso, PermisoViewModel>();
            CreateMap<PermisoViewModel, Permiso>();

        }
    }
}
