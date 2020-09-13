using EncuestasUABC.Enumerador;
using System;

namespace EncuestasUABC.Utilidades
{
    public class MessageAlertException : Exception
    {
        public int ExceptionType { get; set; }
        public MessageAlertException(MessageAlertType exceptionType, string message) : base(message)
        {
            ExceptionType = (int)exceptionType;
        }

    }
}
