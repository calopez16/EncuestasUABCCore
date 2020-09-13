using EncuestasUABC.Constantes;
using EncuestasUABC.Models;
using EncuestasUABC.Utilidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EncuestasUABC.Controllers
{
    public class BaseController : Controller
    {
        public void ShowMessageSuccess(string message)
        {
            #region ShowMessageSucces
            TempData["MessageAlert"] = JsonConvert.SerializeObject(new MessageAlert
            {
                Message = message,
                ClassAlert = Alerts.CLASS_ALERT_SUCCESS,
                ClassIcon = Alerts.CLASS_ICON_ALERT_SUCCESS,
                TitleAlert = Alerts.CLASS_TITLE_ALERT_SUCCESS
            });
            #endregion
        }

        public void ShowMessageInfo(string message)
        {
            #region ShowMessageInfo
            TempData["MessageAlert"] = JsonConvert.SerializeObject(new MessageAlert
            {
                Message = message,
                ClassAlert = Alerts.CLASS_ALERT_INFORMATION,
                ClassIcon = Alerts.CLASS_ICON_ALERT_INFORMATION,
                TitleAlert = Alerts.CLASS_TITLE_ALERT_INFORMATION
            });
            #endregion
        }

        public void ShowMessageWarning(string message)
        {
            #region ShowMessageWarning
            TempData["MessageAlert"] = JsonConvert.SerializeObject(new MessageAlert
            {
                Message = message,
                ClassAlert = Alerts.CLASS_ALERT_WARNING,
                ClassIcon = Alerts.CLASS_ICON_ALERT_WARNING,
                TitleAlert = Alerts.CLASS_TITLE_ALERT_WARNING
            });
            #endregion
        }

        public void ShowMessageDanger(string message)
        {
            #region ShowMessageDanger
            TempData["MessageAlert"] = JsonConvert.SerializeObject(new MessageAlert
            {
                Message = message,
                ClassAlert = Alerts.CLASS_ALERT_DANGER,
                ClassIcon = Alerts.CLASS_ICON_ALERT_DANGER,
                TitleAlert = Alerts.CLASS_TITLE_ALERT_DANGER
            });
            #endregion
        }
        public void ShowMessageException(string message)
        {
            #region ShowMessageException
            TempData["MessageAlert"] = JsonConvert.SerializeObject(new MessageAlert
            {
                Message = message,
                ClassAlert = Alerts.CLASS_ALERT_DANGER,
                ClassIcon = Alerts.CLASS_ICON_ALERT_EXCEPTION,
                TitleAlert = Alerts.CLASS_TITLE_ALERT_EXCEPTION
            });
            #endregion
        }

        public void GenerarAlerta(MessageAlertException ex)
        {
            #region GenerarAlerta
            switch ((int)ex.ExceptionType)
            {
                case (int)Enumerador.MessageAlertType.SUCCESS:
                    ShowMessageSuccess(ex.Message);
                    break;
                case (int)Enumerador.MessageAlertType.INFORMATION:
                    ShowMessageInfo(ex.Message);
                    break;
                case (int)Enumerador.MessageAlertType.WARNING:
                    ShowMessageWarning(ex.Message);
                    break;
                case (int)Enumerador.MessageAlertType.DANGER:
                    ShowMessageDanger(ex.Message);
                    break;
                case (int)Enumerador.MessageAlertType.EXCEPTION:
                    ShowMessageException(ex.Message);
                    break;
            }
            #endregion
        }

    }
}