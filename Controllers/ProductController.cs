
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using mongotest.Data;
using mongotest.Models;

namespace mongotest.Controllers
{

    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        readonly ProductDal _productDAL;

        public ProductController(ProductDal producDal)
        {
            _productDAL = producDal;
        }

        [HttpGet]
        public IEnumerable<Product> getAll() => _productDAL.All();
        
        [HttpGet]
        [Route("{id}")]
        public IActionResult getProduct(string id){
            if(string.IsNullOrEmpty(id))
                return BadRequest("el identificado no puede venir vacío o nulo");
            var prod = this._productDAL.Find(id);;
            if(prod == null)
                return NotFound(new {Message = "El producto no fue encontrado"});

            return Ok(prod);
        }

        [HttpPost]
        public IActionResult addProduct([FromBodyAttribute]Product model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            bool save = this._productDAL.Save(model);
            if(save)
                return Ok(new {Message = "Producto generado"});
            else
                return BadRequest();

        } 

        [HttpPut]
        public IActionResult updateProduct([FromBodyAttribute]Product model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            if(model.id == null)
                return BadRequest(new {Message = "El identificador no debe estar nulo"});

            bool update = this._productDAL.Update(model);

            if(update)
                return Ok(new {Message = $"Producto {model.Name} actualizado correctamente"});
            else
            return BadRequest();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult deleteProduct(string id)
        {
            if(string.IsNullOrEmpty(id))
                return BadRequest("el identificado no puede venir vacío o nulo");

            bool delete = this._productDAL.Remove(id);

            if(delete)
                return Ok("Producto eliminado correctamente");
            else
                return BadRequest();

        }
        
    }
}