using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
 

namespace project.Controllers
{
    [Route("api")]
    [ApiController]

    public class Controller : ControllerBase
    {
        private readonly HttpClient _httpClient = new HttpClient();
       
        [HttpGet("products")]
            public async Task<ActionResult<Object>> GetAllProducts()
            {            
                var products = await this._httpClient.GetFromJsonAsync<Object>("https://fakestoreapi.com/products");
            
                return Ok(products);    
            }



        [HttpPost("products")]
        public ActionResult<Product> PostProduct(Product product)
        {
             Product newProduct = product;
            var result = this._httpClient.PostAsJsonAsync<Object>("https://fakestoreapi.com/products", newProduct);
            Console.WriteLine(result);
            return Ok(result);
        }

    }

    public class Product
    {
        public int id { get; set; }
        public string title { get; set; }
        public string price { get; set; }
        public string category { get; set; }
        public string description { get; set; }
        public string image { get; set; }

        public Product() { }
    }
}
