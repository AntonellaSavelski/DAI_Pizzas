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
                return Ok (Login(MiUsuario));
            }
            return NotFound();
        }

        [HttpGet]
        
        public IActionResult GetAll()
        {
            List<Usuario> ListaUsuarios;
            ListaUsuarios= bd1.GetAll();
            return Ok(ListaUsuarios);
        }

        [HttpGet ("{Id}")]
        public IActionResult GetById(int Id)
        {
            if(Id<=0){
                return BadRequest();
            }else{
                Usuario MiUsuario;
                MiUsuario=bd1.ConsultaUsuario(Id);
                if(MiUsuario==null){
                    return NotFound();
                }else{
                    return Ok(MiUsuario);
                }
            }
        }

        [HttpPost]
        public IActionResult Create(Usuario MiUsuario)
        {
            bd1.AgregarUsuario(MiUsuario);
            return Created("/login", new{Id=MiUsuario.Id});
        }

        [HttpPut ("{id}")]
        public IActionResult Update(int Id, Usuario MiUsuario)
        {
            if(MiUsuario.Id!=Id){
                return BadRequest();
            }else{
                if(bd1.Update(Id,MiUsuario)==null){
                    return NotFound();
                }else{
                    return Ok();
                }
            }
        }

        [HttpDelete ("{id}")]
        public IActionResult Delete(int Id)
        {
            if(Id<=0){
                return BadRequest();
            }else{
                if(bd1.Delete(Id)==null){
                    return NotFound();
                }else{
                    return Ok();
                }
            }
        }
        [HttpGet ("{Id}")]
        public IActionResult GetByUserNamePassword(string UserName, string password)
        {
                Usuario MiUsuario;
                MiUsuario=bd1.GetByUserNamePassword(UserName,password);
                if(MiUsuario==null){
                    return NotFound();
                }else{
                    return Ok(MiUsuario);
                }
        }
    }
}


