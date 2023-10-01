using Azure.Core;
using Ecom.API.Validator;
using Ecom.ClientEntity.Request.Product;
using Ecom.ClientEntity.Response;
using Ecom.Entity.Domain;
using Ecom.Service.productService;
using Ecom.Service.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace Ecom.API.Controllers
{
    [Route("api/")]
    [ApiController]
    //[Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ProductValidator _validator;
        public ProductController(IProductService productService, ProductValidator validator)
        {
            _productService = productService;
            _validator = validator;
        }
        [HttpGet("products")]
        public async Task<IActionResult> GetAllProduct(int page , int pageSize)
        {
            try
            {
                var products = await _productService.GetAllProduct(page, pageSize);

                return products is not null ? Ok(new { isSucces = true, StatusCode = "200", Status = "OK", Message = "Data Retrived Succesfully", Data = products })
                                            : BadRequest(new { isSucces = false, StatusCode = "404", Status = "NotFound", Message = "Items not found", Data = products });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

       
        [HttpGet("products/{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                if(id <= 0)
                {
                    return BadRequest(new { isSucces = false, StatusCode = "400", Status = "BadRequest", Message = "id is invalid", Data = "" });
                }

                var product = await _productService.GetProductById(id);

                return product is not null ? Ok(new { isSucces = true, StatusCode = "200", Status = "OK", Message = "Data Retrived Succesfully", Data = product })
                                           : NotFound(new { isSucces = false, StatusCode = "404", Status = "Not Found", Message = "item not found", Data = product });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("products")]
        public async Task<IActionResult> AddProduct([FromForm] ProductRequest request)
        {
            try
            {
                var validationResult = _validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
               
              

                string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "") ;

                var isAdded = await _productService.Add(request,token);

               return  isAdded ? Ok(new { isSucces = true, StatusCode = "201", Status = "Created", Message = "Data Created Succesfully" ,Data = request})
                               : BadRequest(new { isSuccces = false, StatusCode = 400, Status = "BadRequest", Message = "request not valid" });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
             
        }
        


        [HttpPut("products/{id}")]
        public async  Task<IActionResult> UpdateProduct([FromRoute]int id, [FromForm] ProductRequest product)
        {
            try
            {
                if(id <= 0)
                {
                    return BadRequest(new { isSuccces = false, StatusCode = 400, Status = "BadRequest", Message = "request id is not valid" });
                }
                var validationResult = _validator.Validate(product);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }

                string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                var isUpdated = await _productService.Update(product, id, token);

                return isUpdated ? Ok(new { isSucces = true, StatusCode = "200", Status = "OK", Message = "Data Updated Succesfully", Data = product })
                                 : BadRequest(new { isSuccces = false, StatusCode = 400, Status = "BadRequest", Message = "request not valid" });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("products/{id:int}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            try
            {
                if(id <= 0)
                {
                    return BadRequest(new { isSuccess = false, Status = "Not Found", StatusCode = "404", Message = "requested id is invalid", DataId = id });
                }
                string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                var isDeleted = await _productService.Delete(id,token);

                return isDeleted ? Ok(new {isSuccess = true,Status = "Ok", StatusCode = "200",Message = "Deleted Succefully" , DataId = id})
                                 : BadRequest(new { isSuccess = false, Status = "Bad Request", StatusCode = "400", Message = "id is invalid", DataId = id });


            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
        [HttpPost("products/Active/{id:int}")]

        public async Task<IActionResult> ActiveProduct([FromRoute]int id)
        {
            try
            {
                string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                var isActivate = await _productService.Active(id, token);

                return isActivate ? Ok(new { isSuccces = true, StatusCode = 200, Status = "OK", Message = "activation success" })
                                  : BadRequest(new { isSuccces = false, StatusCode = 400, Status = "BadRequest", Message = "activation request not valid" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("products/inActive/{id:int}")]

        public async Task<IActionResult> InActiveProduct([FromRoute] int id)
        {
            try
            {
                string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                var isInActivate = await _productService.InActive(id, token);

                return isInActivate ? Ok(new { isSuccces = true, StatusCode = 200, Status = "OK", Message = "inactivation success" })
                                    : BadRequest(new { isSuccces = false, StatusCode = 400, Status = "BadRequest", Message = "inactivation request not valid" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
