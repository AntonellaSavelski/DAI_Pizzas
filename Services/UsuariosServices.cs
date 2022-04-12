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

namespace Usuarios.API.Services{
    public class bd1 {
        public static Usuario Login (string UserName, string password){
            //En el caso de que el userName/password exista, realiza un "RefreshToken"  y retorna el Usuario.
            //Sino retorna null.
            Usuario MiUsuario = null;
            using (SqlConnection db = basededatos.GetConnection()){
                string sp = "sp_Login";
                MiUsuario = db.QueryFirstOrDefault<Usuario>(sp, new {pUserName = UserName, pPassword = password},
                commandType: CommandType.StoredProcedure);
            }
            return MiUsuario;
        }
        public static Usuario GetByUserNamePassword(string UserName, string password){
            Usuario MiUsuario = null;
            using (SqlConnection db = basededatos.GetConnection()){
                string sp = "sp_GetByUserNamePassword";
                MiUsuario = db.QueryFirstOrDefault<Usuario>(sp, new {pUserName = UserName, pPassword = password},
                commandType: CommandType.StoredProcedure);
            }
            return MiUsuario;
        }
        public static Usuario GetByToken(string token) {
            Usuario MiToken = null;
            string sp = "sp_GetByToken";
            using (SqlConnection db = basededatos.GetConnection()){
               MiToken = db.QueryFirstOrDefault<Usuario>(sp, new {pToken= token}, 
               commandType: CommandType.StoredProcedure);
            } 
            return MiToken;
        }

        public static Usuario RefreshToken(int id) {
            Usuario MiId = null;
            string sp = "sp_RefreshToken";
            using (SqlConnection db = basededatos.GetConnection()){
               MiToken = db.Execute(sp, new {pId= InvalidTimeZoneException}, 
               commandType: CommandType.StoredProcedure);
            } 
            return MiId;

        // Actualiza el "Token" del Usuario cuyo Id es enviado por parámetro por un 
        //nuevo token y su "TokenExpirationDate" +15 minutos de la fecha/hora actual. Sino retorna null.
        }
        public static List<Usuario> GetAll(){
            List<Usuario> ListaUsuarios;
            string sp= "SELECT * FROM Usuarios";
            using(SqlConnection BD=basededatos.GetConnection()){
                ListaUsuarios = BD.Query<Usuario>(sp).ToList();
            }
            return ListaUsuarios;
        }


        public static Usuario ConsultaUsuario(int Id){
            Usuario MiUsuario = null;
            string sp = "SELECT * FROM Usuarios WHERE Id=@pId";
            using(SqlConnection BD=basededatos.GetConnection()){
                MiUsuario = BD.QueryFirstOrDefault<Usuario>(sp, new{ pId = Id});
            }
            return MiUsuario;
        }

        public static Usuario AgregarUsuario(Usuario MiUsuario){
            string sp = "INSERT INTO Usuarios (Nombre,Apellido,UserName,Passwordd,Token) Values(@pNombre,@pApellido,@pUserName,@pPasswordd,@pToken)";
            int temporizador =0;
            using(SqlConnection BD=basededatos.GetConnection()){
                temporizador = BD.Execute(sp, new{ pNombre=MiUsuario.Nombre,pApellido=MiUsuario.Apellido,pUserName=MiUsuario.UserName,pPasswordd=MiUsuario.Passwordd, pToken=MiUsuario.Token});
            }
            return new Usuario();
        }

        public static Usuario Update(int Id, Usuario MiUsuario){
            Usuario UsuarioLocal;
            string sp = "UPDATE Usuarios SET  Nombre=@pNombre,Apellido=@pApellido,UserName=@pUserName,Passwordd=@pPasswordd,Token=@pToken WHERE id=@pid";
            string sp2 = "SELECT * FROM Usuarios WHERE Id=@pId";
            int temp=0;

            using(SqlConnection =bd.basededatos.GetConnection()){
                UsuarioLocal = bd.QueryFirstOrDefault<Usuario>(sp2, new{ pId = Id});
            }
            if(UsuarioLocal==null){
                return UsuarioLocal;
            }else{
                    using(SqlConnection BD=basededatos.GetConnection()){
                        temp = BD.Execute(sp, new{pId = Id, pNombre=MiUsuario.Nombre,pApellido=MiUsuario.Apellido,pUserName=MiUsuario.UserName,pPasswordd=MiUsuario.Passwordd, pToken=MiUsuario.Token});
                    }
                return new Usuario();
                }
        }

        public static Usuario Delete(int Id){
            Usuario MiUsuario=null;
            string sp = "DELETE FROM Usuarios WHERE Id=@pid";
            string sp2 = "SELECT * FROM Usuarios WHERE Id=@pId";
            int temporizador=0;

            using(SqlConnection bd=basededatos.GetConnection()){
                MiUsuario = bd.QueryFirstOrDefault<Usuario>(sp2, new{ pId = Id});
            }
            if(MiUsuario==null){
                return MiUsuario;
            }else{
                using(SqlConnection bd=basededatos.GetConnection()){
                temporizador = bd.Execute(sp, new{pId = Id});
            }
                return new Usuario();
            }
        }
    }
}