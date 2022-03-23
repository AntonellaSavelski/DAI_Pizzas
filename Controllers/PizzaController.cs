using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pizzas.API.Models;
using System.Data.SqlClient;
using Dapper;

namespace Pizzas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PizzaController : ControllerBase
    {  
        public static List<Pizza> _ListadoPizzas = new List<Pizza>();

        [HttpGet]
        public IActionResult GetAll()
        {
            using (SqlConnection db = new SqlConnection(basededatos._connectionString)){
                string sql = "SELECT * from Pizzas";
                _ListadoPizzas = db.Query<Pizza>(sql).ToList();
            }
            return Ok (_ListadoPizzas);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id){
            Pizza unaPizza;
            using (SqlConnection db = new SqlConnection(basededatos._connectionString)){
                string sql = "SELECT * from Pizzas WHERE id = @pId";
                unaPizza = db.QueryFirstOrDefault<Pizza>(sql, new {pId = id});
            }
            return Ok(unaPizza);
        }

        [HttpPost]
        public IActionResult Create(Pizza pizza){
            int nuevaPizza;
            string sql = "INSERT into Pizzas (Nombre, LibreGluten, Importe, Descripcion) VALUES (@pNombre, @pLibreGluten, @pImporte, @pDescripcion)";
            using (SqlConnection db = new SqlConnection(basededatos._connectionString)){
               nuevaPizza = db.Execute(sql, new {pNombre=pizza.Nombre, pLibreGluten=pizza.LibreGluten, pImporte=pizza.Importe, pDescripcion=pizza.Descripcion});
            } 
            return Ok(pizza);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Pizza pizza){
            int nuevaPizza;
            string sql = "UPDATE Pizzas SET Nombre = @pNombre, LibreGluten = @pLibreGluten, Importe = @pImporte, Descripcion = @pDescripcion WHERE id = @pId";
            using (SqlConnection db = new SqlConnection(basededatos._connectionString)){
               nuevaPizza = db.Execute(sql, new {pId= id, pNombre=pizza.Nombre, pLibreGluten=pizza.LibreGluten, pImporte=pizza.Importe, pDescripcion=pizza.Descripcion});
            } 
            return Ok(pizza);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id){
            Pizza pizzaABorrar;
            string sql = "DELETE from Pizzas WHERE id = @pId";
            using (SqlConnection db = new SqlConnection(basededatos._connectionString)){
               pizzaABorrar = db.QueryFirstOrDefault<Pizza>(sql, new {pId = id});
            } 
            return Ok(pizzaABorrar);
        }

    }
}
