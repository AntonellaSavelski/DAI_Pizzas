using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizzas.API.Models;
using System.Data.SqlClient;
using Dapper;
using Pizzas.API.Utils;
using Pizzas.API.Helpers;

namespace Pizzas.API.Services{
    public class PizzasServices{
        public static List<Pizza> _ListadoPizzas = new List<Pizza>();

        public IActionResult GetAll()
        {
            using (SqlConnection db = basededatos.GetConnection()){
                string sp = "sp_GetAll";
                _ListadoPizzas = db.Query<Pizza>(sp, commandType: CommandType.StoredProcedure).ToList();
            }
            return Ok (_ListadoPizzas);
        }
        public IActionResult GetById(int id){
            Pizza unaPizza;
            using (SqlConnection db = basededatos.GetConnection()){
                string sp = "sp_GetById";
                unaPizza = db.QueryFirstOrDefault<Pizza>(sp, new {pId = id}, 
                commandType: CommandType.StoredProcedure);
            }
            return Ok(unaPizza);
        }
        public IActionResult Create(Pizza pizza){
            int nuevaPizza;
            string sp = "sp_Create";
            using (SqlConnection db = basededatos.GetConnection()){
               nuevaPizza = db.Execute(sp, new {pNombre=pizza.Nombre, pLibreGluten=pizza.LibreGluten, pImporte=pizza.Importe, pDescripcion=pizza.Descripcion},
               commandType: CommandType.StoredProcedure);
            } 
            return Ok(pizza);
        }
        public IActionResult Update(int id, Pizza pizza){
            int nuevaPizza;
            string sp = "sp_Update";
            using (SqlConnection db = basededatos.GetConnection()){
               nuevaPizza = db.Execute(sp, new {pId= id, pNombre=pizza.Nombre, pLibreGluten=pizza.LibreGluten, pImporte=pizza.Importe, pDescripcion=pizza.Descripcion}, 
               commandType: CommandType.StoredProcedure);
            } 
            return Ok(pizza);
        }
        public IActionResult DeleteById(int id){
            Pizza pizzaABorrar;
            string sp = "sp_DeleteById";
            using (SqlConnection db = basededatos.GetConnection()){
               pizzaABorrar = db.QueryFirstOrDefault<Pizza>(sp, new {pId = id}, 
               commandType: CommandType.StoredProcedure);
            } 
            return Ok(pizzaABorrar);
        }
    }
}