using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module1.Model;

namespace Module1.Controllers
{
//    [Produces("application/json")]
    [Route("api/Product")]
    public class ProductController : Controller
    {
        static List<Product> _products = new List<Product>()
        {
            new Product(){Id = 0,ProductName = "Laptop",Price = "200"},
            new Product(){Id = 1,ProductName = "PC",Price = "800"},
            new Product(){Id = 2,ProductName = "Mobile",Price = "100"}
        };

        [HttpGet("ShowWelcomeMsg")]
        public IActionResult GetWelcomeMessage()
        {
            return Ok("Hello World!");
        }
        [HttpGet]
        public IEnumerable<Product> GetProduct()
        {
            return _products;
        }
        [HttpGet("GetProdWithReturnStatus")]
        public IActionResult GetProd()
        {
            //return NotFound();
            //return BadRequest();
            return Ok(_products);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            _products.Add(product);
            return StatusCode(StatusCodes.Status201Created);
        }


        [HttpPut("{id}")]
        public void PutProduct([FromRoute] int id, [FromBody] Product product)
        {
            _products[id] = product;
        }

        [HttpDelete("{id}")]
        public void DeleteProduct([FromRoute] int id)
        {
            _products.RemoveAt(id);
        }
    }
}