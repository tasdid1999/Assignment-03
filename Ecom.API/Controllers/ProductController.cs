using Ecom.ClientEntity.Request.Product;
using Ecom.ClientEntity.Response;
using Ecom.Entity.Domain;
using Ecom.Service.productService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("products")]
        public async Task<ActionResult> GetAllProduct(int page , int pageSize)
        {
            var products = await _productService.GetAllProduct(page, pageSize);

            return Ok(new { isSucces = true , StatusCode = "200", Status = "OK", Message = "Data Consume Successfull", Data = products });
        }

       
        [HttpGet("products/{id:int}")]
        public async Task<ActionResult> GetProductById(int id)
        {
            try
            {
                if(id <= 0)
                {
                    return BadRequest();
                }

                var product = await _productService.GetProductById(id);

                return product is null ? Ok(new { isSucces = true, StatusCode = "200", Status = "OK", Message = "Data Consume Successfull", Data = product })
                                           : NotFound(new { isSucces = false, StatusCode = "404", Status = "Not Found", Message = "item not found", Data = product });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       
        [HttpPost("products")]
        public ActionResult Post([FromForm] ProductRequest request)
        {
            return Ok();
        }

       
        [HttpPut("products/{id}")]
        public ActionResult Put(int id, [FromBody] ProductRequest product)
        {
          

            return Ok();
        }


        [HttpDelete("products/{id}")]
        public async Task<ActionResult> DeleteProduct([FromRoute] int id)
        {
            try
            {
                if(id <= 0)
                {
                    return BadRequest();
                }
                var isDeleted = await _productService.Delete(id);

                return isDeleted ? Ok() : BadRequest();


            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
    }
}
