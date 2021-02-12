using AutoMapper;
using EncuestasUABC.Models;
using EncuestasUABC.Models.ViewModels;
using EncuestasUABC.Models.ViewModels.Catalogos;

namespace EncuestasUABC
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserViewModel>();
            CreateMap<ApplicationUserViewModel, ApplicationUser>();

            CreateMap<Permiso, PermisoViewModel>();
            CreateMap<PermisoViewModel, Permiso>();

            CreateMap<Encuesta, EncuestaViewModel>();
            CreateMap<EncuestaViewModel, Encuesta>();

            CreateMap<EncuestaSeccion, EncuestaSeccionViewModel>();
            CreateMap<EncuestaSeccionViewModel, EncuestaSeccion>();

            CreateMap<Carrera, CarreraViewModel>();
            CreateMap<CarreraViewModel, Carrera>();

            CreateMap<UnidadAcademica, UnidadAcademicaViewModel>();
            CreateMap<UnidadAcademicaViewModel, UnidadAcademica>();

            CreateMap<Campus, CampusViewModel>();
            CreateMap<CampusViewModel, Campus>();

            CreateMap<EncuestaPregunta, EncuestaPreguntaViewModel>();
            CreateMap<EncuestaPreguntaViewModel, EncuestaPregunta>();

            CreateMap<EncuestaPreguntaOpcion, EncuestaPreguntaOpcionViewModel>();
            CreateMap<EncuestaPreguntaOpcionViewModel, EncuestaPreguntaOpcion>();

            CreateMap<TipoPregunta, TipoPreguntaViewModel>();
            CreateMap<TipoPreguntaViewModel, TipoPregunta>();

        }
    }
}
