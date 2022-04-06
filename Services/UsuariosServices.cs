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
    public class UsuariosServices {
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
    }
}