namespace EncuestasUABC.Constantes
{
    public static class Contrasena
    {
        public static int MinimoCaracteresPermitidos = 6;
        public static bool RequiereNumero = true;
        public static bool RequiereMinusculas = true;
        public static bool RequiereMayusculas = true;
        public static bool RequiereCaracteresEspeciales = true;
        public static int MinimoCaracteresUnicos = 1;
    }
    public static class RolesSistema
    {
        public static string Administrador = "Administrador";
        public static string Usuario = "Usuario";
    }  

    public static class EstatusEncuestaAsignada
    {
        public const string SinVisualizar = "SIN VISUALIZAR";
        public const string Iniciada = "INICIADA";
        public const string Finalizada = "FINALIZADA";
    }
    public static class EstatusEncuesta
    {
        public static string Activa = "ACTIVA";
        public static string Inactiva = "INACTIVA";
        public static string Eliminada = "ELIMINADA";

    }
    public static class TipoPregunta
    {
        public static string Abierta = "ABIERTA";
        public static string Multiple = "OPCIÓN MÚLTIPLE";
        public static string UnicaOpcion = "ÚNICA OPCIÓN";

    }
 
    public static class Defaults
    {
        public static string Contrasena = "Us@2020!";
        public static string AdminEmail = "admin@admin";
        public static string AdminPass = "Admin123!";
    }

    public static class Alerts
    {
        public static string CLASS_ALERT_SUCCESS = "success";
        public static string CLASS_ALERT_INFORMATION = "info";
        public static string CLASS_ALERT_WARNING = "warning";
        public static string CLASS_ALERT_DANGER = "danger";

        public static string CLASS_ICON_ALERT_SUCCESS = "check_circle";
        public static string CLASS_ICON_ALERT_INFORMATION = "info";
        public static string CLASS_ICON_ALERT_WARNING = "warning";
        public static string CLASS_ICON_ALERT_DANGER = "report";
        public static string CLASS_ICON_ALERT_EXCEPTION = "error";

        public static string CLASS_TITLE_ALERT_SUCCESS = "¡Operación exitosa!";
        public static string CLASS_TITLE_ALERT_INFORMATION = "Información";
        public static string CLASS_TITLE_ALERT_WARNING = "Aviso";
        public static string CLASS_TITLE_ALERT_DANGER = "¡Atención!";
        public static string CLASS_TITLE_ALERT_EXCEPTION = "Ocurrió un error en la aplicación, favor de contactar al administrador";
    }

    public static class Mensajes
    {
        public static string Login_MSJ01 = "El usuario y/o contraseña son incorrectos.";
        public static string Login_MSJ02 = "El usuario se encuentra deshabilitado.";
        public static string Login_MSJ03 = "El usuario y/o contraseña son incorrectos.";
        public static string Login_MSJ04 = "Ocurrió un error al iniciar sesión.";

        public static string USUARIOS_MSJ01 = "El usuario <strong>{0}</strong> se registró correctamente.";
        public static string USUARIOS_MSJ02 = "Ocurrió un error al registrar al usuario.";
        public static string USUARIOS_MSJ03 = "El correo <strong>{0}</strong> ya se encuentra registrado en el sistema.";
        public static string USUARIOS_MSJ04 = "El usuario se creó correctamente, pero no se pudo asignar Rol.";
        public static string USUARIOS_MSJ05 = "El usuario no se pudo registrar correctamente.";
        public static string USUARIOS_MSJ06 = "El usuario <strong>{0}</strong> se modificó correctamente.";
        public static string USUARIOS_MSJ07 = "El usuario no se encontró en el sistema.";
        public static string USUARIOS_MSJ08 = "El usuario <strong>{0}</strong> no se pudo modificar correctamente.";
        public static string USUARIOS_MSJ09 = "El usuario <strong>{0}</strong> se eliminó correctamente.";
        public static string USUARIOS_MSJ10 = "El usuario <strong>{0}</strong> se restauró correctamente.";
        public static string USUARIOS_MSJ11 = "El usuario <strong>{0}</strong> no se pudo eliminar correctamente.";
        public static string USUARIOS_MSJ12 = "El usuario <strong>{0}</strong> no se pudo restaurar correctamente.";
        public static string USUARIOS_MSJ13 = "Ocurrió un error al asignar el rol.";
        public static string USUARIOS_MSJ14 = "La constraseña se cambió correctamente, favor de iniciar sesión con la nueva contraseña.";
        public static string USUARIOS_MSJ15 = "La constraseña no se pudo cambiar correctamente.";
        public static string USUARIOS_MSJ16 = "La constraseña se cambió correctamente.";
        public static string Usuarios_Msj17 = "Los permisos del usuario <strong>{0}</strong> se han actualizado correctamente.";
        public static string Usuarios_Msj18 = "No se han podido actualizar los permisos del usuario <strong>{0}</strong>.";
        public static string Usuarios_Msj19 = "La constraseña no coincide con la actual.";

        public static string Usuarios_Msj20 = $"La contraseña deben tener un largo mínimo de {Contrasena.MinimoCaracteresPermitidos} caracteres.";
        public static string Usuarios_Msj21 = "La contraseña debe incluir al menos un dígito ('0'-'9').";
        public static string Usuarios_Msj22 = "La contraseña debe incluir al menos una letra MAYÚSCULA ('A'-'Z').";
        public static string Usuarios_Msj23 = "La contraseña debe incluir al menos una letra minúscula ('a'-'z').";
        public static string Usuarios_Msj24 = "La constraseña debe contener al menos un caracter alfanumérico.";
        public static string Usuarios_Msj25 = "La constraseña no es válida.";

        public static string ENCUESTAS_MSJ01 = "La Encuesta se creó correctamente.";
        public static string ENCUESTAS_MSJ02 = "La Encuesta no se pudo crear correctamente.";
        public static string ENCUESTAS_MSJ03 = "La Encuesta se editó correctamente.";
        public static string ENCUESTAS_MSJ04 = "La Encuesta no se pudo editar correctamente.";
        public static string ENCUESTAS_MSJ05 = "La Sección se agregó correctamente.";
        public static string ENCUESTAS_MSJ06 = "La Sección no se pudo agregar correctamente.";
        public static string Encuesta_msj07 = "La encuesta se eliminó correctamente.";
        public static string Encuesta_msj08 = "La encuesta no pudo eliminarse correctamente.";
        public static string Encuesta_msj09 = "La encuesta se restauró correctamente.";
        public static string Encuesta_msj10 = "La encuesta no pudo restaurar correctamente.";
        public static string Encuesta_msj11 = "La encuesta no contiene secciones.";

    }
}