
namespace EncuestasUABC.Enumerador
{
    public enum CampusId
    {
        Mexicali = 1,
        Ensenada,
        Tijuana,
        Tecate
    }
    public enum EstatusEncuestaId
    {
        Activa = 1,
        Inactiva,
        Eliminada
    }
    public enum TipoPreguntaId
    {
        Abierta = 1,
        Multiple,
        UnicaOpcion,
        Condicional,
        Matriz,
        SelectList
    }

    public enum MessageAlertType
    {
        Success = 1,
        Warning,
        Information,
        Danger,
        Exception
    }

    public enum RolId
    {
        Administrador = 1,
        Administrativo,
        Coordinador,
        Tutor,
        Alumno,
        Egresado
    }
}