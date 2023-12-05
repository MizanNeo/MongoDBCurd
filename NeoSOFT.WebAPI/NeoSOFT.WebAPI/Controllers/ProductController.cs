using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NeoSOFT.Application.Contracts;
using NeoSOFT.Domain.DTO;

namespace NeoSOFT.WebAPI.Controllers
{
   
    [ApiController]
    public class ProductController : AnonymousBaseController
    {
        protected IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get All Product Details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetAll))]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetAll());
        }

        /// <summary>
        /// Get product By id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetById))]
        public async Task<IActionResult> GetById([FromQuery] string id)
        {
            return Ok(await _productService.GetById(id));
        }

        /// <summary>
        /// Add new product
        /// </summary>
        /// <param name="ProductDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(Create))]
        public async Task<IActionResult> Create([FromBody] ProductDto productDto)
        {
            return Ok(await _productService.Create(productDto));
        }

        /// <summary>
        /// Update Product Details
        /// </summary>
        /// <param name="ProductDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(Update))]
        public async Task<IActionResult> Update([FromBody] ProductDto productDto)
        {
            return Ok(await _productService.Update(productDto));
        }

        /// <summary>
        /// Delete Product Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route(nameof(Delete))]
        public async Task<IActionResult> Delete([FromQuery] string id)
        {
            return Ok(await _productService.Delete(id));
        }
    }
}
