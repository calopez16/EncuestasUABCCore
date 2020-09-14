using EncuestasUABC.Enumerador;
using System;

namespace EncuestasUABC.Utilidades
{
    public class MessageException : Exception
    {
        public int ExceptionType { get; set; }
        public MessageException(MessageAlertType exceptionType, string message) : base(message)
        {
            ExceptionType = (int)exceptionType;
        }

    }
}
