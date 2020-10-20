using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using EncuestasUABC.ViewModels;
using System.Collections.Generic;
using EncuestasUABC.Models;

namespace EncuestasUABC
{
    public static class HttpContextExtensions
    {
        public static UsuarioSessionInfoViewModel GetUsuarioInfoViewModel(this IHttpContextAccessor httpContextAssesor)
        {
            #region GetUsuarioInfoViewModel
            UsuarioSessionInfoViewModel usuarioInfo = new UsuarioSessionInfoViewModel();
            string usuarioInfoJson = httpContextAssesor.HttpContext.Session.GetString("UsuarioInfo");
            if (!string.IsNullOrEmpty(usuarioInfoJson))
            {
                usuarioInfo = JsonConvert.DeserializeObject<UsuarioSessionInfoViewModel>(httpContextAssesor.HttpContext.Session.GetString("UsuarioInfo"));
            }
            #endregion
            return usuarioInfo;
        }

        public static List<PermisoViewModel> GetUsuarioPermisosMenu(this IHttpContextAccessor httpContextAssesor)
        {
            #region GetUsuarioInfoViewModel
            List<PermisoViewModel> permiso = new List<PermisoViewModel>();
            string usuarioInfoJson = httpContextAssesor.HttpContext.Session.GetString("UsuarioInfo");
            if (!string.IsNullOrEmpty(usuarioInfoJson))
            {
                var usuario = JsonConvert.DeserializeObject<UsuarioSessionInfoViewModel>(httpContextAssesor.HttpContext.Session.GetString("UsuarioInfo"));
                permiso = usuario.PermisosMenu;
            }
            #endregion
            return permiso;
        }

        public static string GetUsuarioId(this IHttpContextAccessor httpContextAssesor)
        {
            #region GetUsuarioInfoViewModel
            UsuarioSessionInfoViewModel usuarioInfo = new UsuarioSessionInfoViewModel();
            string usuarioInfoJson = httpContextAssesor.HttpContext.Session.GetString("UsuarioInfo");
            if (!string.IsNullOrEmpty(usuarioInfoJson))
            {
                usuarioInfo = JsonConvert.DeserializeObject<UsuarioSessionInfoViewModel>(httpContextAssesor.HttpContext.Session.GetString("UsuarioInfo"));
            }
            #endregion
            return usuarioInfo.Id;
        }
    }
}
