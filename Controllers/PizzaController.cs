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
using Pizzas.API.Services;
using Pizzas.API.Helpers;

namespace Pizzas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PizzaController : ControllerBase
    {  
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok (PizzasServices.GetAll(_ListadoPizzas));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id){
            
            return Ok(PizzasServices.GetById(unaPizza));
        }

        [HttpPost]
        public IActionResult Create(Pizza pizza){
        
            return Ok(PizzasServices.Create(pizza));
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Pizza pizza){
            int nuevaPizza;
            string sql = "UPDATE Pizzas SET Nombre = @pNombre, LibreGluten = @pLibreGluten, Importe = @pImporte, Descripcion = @pDescripcion WHERE id = @pId";
            using (SqlConnection db = basededatos.GetConnection()){
               nuevaPizza = db.Execute(sql, new {pId= id, pNombre=pizza.Nombre, pLibreGluten=pizza.LibreGluten, pImporte=pizza.Importe, pDescripcion=pizza.Descripcion});
            } 
            return Ok(pizza);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id){
            Pizza pizzaABorrar;
            string sql = "DELETE from Pizzas WHERE id = @pId";
            using (SqlConnection db = basededatos.GetConnection()){
               pizzaABorrar = db.QueryFirstOrDefault<Pizza>(sql, new {pId = id});
            } 
            return Ok(pizzaABorrar);
        }

    }
}
