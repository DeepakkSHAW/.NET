using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MyShop.Model;
namespace MyShop.Controllers
{
    [Produces("application/json")]
    [Route("api/Products")]
    public class ProductsController : Controller
    {
        private static List<Products> _products = new List<Products>()
        {
            new Products() {ProdID = 1, ProductName = "Gold"},
            new Products() {ProdID = 2, ProductName = "Silver"},
        };

        [HttpGet]
        [Route("getAProduct/{id}")]
        public IActionResult GetaProduct([FromRoute] int id)
        {
            Products p = _products.Find(item => item.ProdID == id);
            if (p == null)
                return StatusCode(StatusCodes.Status204NoContent);
            else
                return Ok(p);
        }

        [HttpGet("getAllProducts")]
        public IActionResult GetallProducts()
        {
            return Ok(_products);
        }

        [HttpPost]
        public IActionResult PostaProduct([FromBody] Products p)
        {
            _products.Add(p);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{ID}")]
        public IActionResult PutaProduct([FromRoute] int id, [FromBody] Products p)
        {
            Products p1 = _products.Find(item => item.ProdID == id);
            if (p1 != null)
            {
                _products[id] = p;
                return StatusCode(StatusCodes.Status202Accepted);
            }
            else
            {
                return StatusCode(StatusCodes.Status406NotAcceptable);
            }
        }

        [HttpDelete("{ID}")]
        public IActionResult DeleteProduct([FromRoute] int id)
        {
            try
            {
                _products.RemoveAt(id);
                return StatusCode(StatusCodes.Status202Accepted);
            }
            catch
            {
                return StatusCode(StatusCodes.Status406NotAcceptable);
            }

        }
    }
}