using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace EncuestasUABC.Utilidades
{
    public static class EnumHelper
    {
        /// <summary>
        /// Permite serializar un Enumerador para poder ser utilizado desde el HTML.
        /// Referencia: https://stackoverflow.com/questions/54167188/how-to-pass-and-use-c-sharp-enum-list-to-javascript-file-and-use-in-it
        /// </summary>
        /// <typeparam name="T">El nombre del Enumerador que se desea serializar.</typeparam>
        /// <returns></returns>
        public static HtmlString EnumToString<T>()
        {
            var values = Enum.GetValues(typeof(T)).Cast<int>();
            var enumDictionary = values.ToDictionary(value => Enum.GetName(typeof(T), value));
            return new HtmlString(JsonConvert.SerializeObject(enumDictionary));
        }
    }
}
