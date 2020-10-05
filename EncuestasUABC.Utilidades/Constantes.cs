namespace EncuestasUABC.Constantes
{
    public static class RolesSistema
    {
        public const string Administrador = "Administrador";
        public const string Maestro = "Maestro";
        public const string Coordinador = "Coordinador";
        public const string Tutor = "Tutor";
        public const string Alumno = "Alumno";
        public const string Egresado = "Egresado";
    }  

    public static class EstatusEncuestaAsignada
    {
        public const string SinVisualizar = "SIN VISUALIZAR";
        public const string Iniciada = "INICIADA";
        public const string Finalizada = "FINALIZADA";
    }
    public static class EstatusEncuesta
    {
        public const string Activa = "ACTIVA";
        public const string Inactiva = "INACTIVA";
        public const string Eliminada = "ELIMINADA";

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
        public static string AdminEmail = "admin@uabc.edu.mx";

    }

    public static class Alerts
    {
        public const string CLASS_ALERT_SUCCESS = "success";
        public const string CLASS_ALERT_INFORMATION = "info";
        public const string CLASS_ALERT_WARNING = "warning";
        public const string CLASS_ALERT_DANGER = "danger";

        public const string CLASS_ICON_ALERT_SUCCESS = "check_circle";
        public const string CLASS_ICON_ALERT_INFORMATION = "info";
        public const string CLASS_ICON_ALERT_WARNING = "warning";
        public const string CLASS_ICON_ALERT_DANGER = "report";
        public const string CLASS_ICON_ALERT_EXCEPTION = "error";

        public const string CLASS_TITLE_ALERT_SUCCESS = "¡Operación exitosa!";
        public const string CLASS_TITLE_ALERT_INFORMATION = "Información";
        public const string CLASS_TITLE_ALERT_WARNING = "Aviso";
        public const string CLASS_TITLE_ALERT_DANGER = "¡Atención!";
        public const string CLASS_TITLE_ALERT_EXCEPTION = "Ocurrió un error en la aplicación, favor de contactar al administrador";
    }

    public static class Mensajes
    {
        public const string USUARIOS_MSJ01 = "El usuario <strong>{0}</strong> se registró correctamente.";
        public const string USUARIOS_MSJ02 = "Ocurrió un error al registrar al usuario.";
        public const string USUARIOS_MSJ03 = "El correo <strong>{0}</strong> ya se encuentra registrado en el sistema.";
        public const string USUARIOS_MSJ04 = "El usuario se creó correctamente, pero no se pudo asignar Rol.";
        public const string USUARIOS_MSJ05 = "El usuario no se pudo registrar correctamente.";
        public const string USUARIOS_MSJ06 = "El usuario <strong>{0}</strong> se modificó correctamente.";
        public const string USUARIOS_MSJ07 = "El usuario no se encontró en el sistema.";
        public const string USUARIOS_MSJ08 = "El usuario <strong>{0}</strong> no se pudo modificar correctamente.";
        public const string USUARIOS_MSJ09 = "El usuario <strong>{0}</strong> se eliminó correctamente.";
        public const string USUARIOS_MSJ10 = "El usuario <strong>{0}</strong> se restauró correctamente.";
        public const string USUARIOS_MSJ11 = "El usuario <strong>{0}</strong> no se pudo eliminar correctamente.";
        public const string USUARIOS_MSJ12 = "El usuario <strong>{0}</strong> no se pudo restaurar correctamente.";
        public const string USUARIOS_MSJ13 = "Ocurrió un error al asignar el rol.";
        public const string USUARIOS_MSJ14 = "La constraseña se cambió correctamente, favor de iniciar sesión con la nueva contraseña.";
        public const string USUARIOS_MSJ15 = "La constraseña no se puedo cambiar correctamente.";
        public const string USUARIOS_MSJ16 = "La constraseña se cambió correctamente.";
        public const string Usuarios_Msj17 = "Los permisos del usuario <strong>{0}</strong> se han actualizado correctamente.";
        public const string Usuarios_Msj18 = "No se han podido actualizar los permisos del usuario <strong>{0}</strong>.";

        public const string ENCUESTAS_MSJ01 = "La Encuesta se creó correctamente.";
        public const string ENCUESTAS_MSJ02 = "La Encuesta no se pudo crear correctamente.";
        public const string ENCUESTAS_MSJ03 = "La Encuesta se editó correctamente.";
        public const string ENCUESTAS_MSJ04 = "La Encuesta no se pudo editar correctamente.";
        public const string ENCUESTAS_MSJ05 = "La Sección se agregó correctamente.";
        public const string ENCUESTAS_MSJ06 = "La Sección no se pudo agregar correctamente.";
        public const string Encuesta_msj07 = "La encuesta se eliminó correctamente.";
        public const string Encuesta_msj08 = "La encuesta no pudo eliminarse correctamente.";
        public const string Encuesta_msj09 = "La encuesta se restauró correctamente.";
        public const string Encuesta_msj10 = "La encuesta no pudo restaurar correctamente.";
    }
}