﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EncuestasUABC.Models.Usuarios
{
    public class CambiarContrasenaViewModel
    {
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
