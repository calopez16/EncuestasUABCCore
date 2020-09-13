
namespace EncuestasUABC.Enumerador
{
    public enum Campus
    {
        MEXICALI = 1,
        ENSENADA,
        TIJUANA,
        TECATE
    }
    public enum EstatusEncuesta
    {
        ACTIVA = 1,
        INACTIVA,
        ELIMINADA
    }
    public enum TipoPregunta
    {
        ABIERTA = 1,
        MULTIPLE,
        UNICA_OPCION,
        CONDICIONAL,
        MATRIZ,
        SUBPREGUNTA
    }

    public enum MessageAlertType
    {
        SUCCESS = 1,
        WARNING,
        INFORMATION,
        DANGER,
        EXCEPTION
    }

}