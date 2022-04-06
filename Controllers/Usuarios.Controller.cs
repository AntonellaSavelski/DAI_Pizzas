using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Usuarios.API.Models;
using System.Data.SqlClient;
using Dapper;
using Pizzas.API.Utils;
using Pizzas.API.Services;
using Pizzas.API.Helpers;

namespace Usuarios.API.Controllers{

    [Route("login")]

    public class UsuariosController : ControllerBase
    { 
        [HttpPost]
        public IActionResult Login(Usuario MiUsuario){
            if(MiUsuario.Id > 0){
                MiUsuario.TokenExpirationDate = MiUsuario.TokenExpirationDate +15;
                return Ok (UsuariosController.Login(MiUsuario));
            }
            return NotFound();
        }

        [HttpGet]
        public static Usuario GetByUserNamePassword(string UserName, string password){
            if (UserName == null){
                return NotFound();
            }
            return Ok (UsuariosController.GetByUserNamePassword(MiUsuario));
        }
    }
}

