using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using EncuestasUABC.Models.Catalogos;
using EncuestasUABC.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace EncuestasUABC.Extensions
{
    public static class HttpContextExtensions
    {
        public static UsuarioInfoViewModel GetUsuarioInfoViewModel(this HttpContext httpContext)
        {
            #region GetUsuarioInfoViewModel
            UsuarioInfoViewModel usuarioInfo = new UsuarioInfoViewModel();
            string usuarioInfoJson = httpContext.Session.GetString("UsuarioInfo");
            if (!string.IsNullOrEmpty(usuarioInfoJson))
            {
                usuarioInfo = JsonConvert.DeserializeObject<UsuarioInfoViewModel>(httpContext.Session.GetString("UsuarioInfo"));
            }
            #endregion
            return usuarioInfo;
        }

        public static List<Permiso> GetUsuarioPermisosMenu(this HttpContext httpContext)
        {
            #region GetUsuarioInfoViewModel
            List<Permiso> permiso = new List<Permiso>();
            string usuarioInfoJson = httpContext.Session.GetString("UsuarioInfo");
            if (!string.IsNullOrEmpty(usuarioInfoJson))
            {
                var usuario = JsonConvert.DeserializeObject<UsuarioInfoViewModel>(httpContext.Session.GetString("UsuarioInfo"));
                permiso = usuario.Permisos.Where(x=>x.Menu).ToList();
            }
            #endregion
            return permiso;
        }
    }
}
