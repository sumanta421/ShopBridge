using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopBridge.BAL;
using ShopBridge.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IShopBridgeBAL _iShopBiz;
        public ProductController(IShopBridgeBAL shopBridgeBAL)
        {
            _iShopBiz = shopBridgeBAL;
        }
        /// <summary>
        /// Gets all Product by Pagination(specifying page number/page Size) / Without Pagination(default)
        /// </summary>
        /// <param name="pageSize">Page Size</param>
        /// <param name="pageNumber">Page Number</param>
        /// <returns></returns>
        [Route("GetAll")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductItem>),200)]
        public async Task<IActionResult> GetAllProducts(int? pageSize=null,int? pageNumber=null)
        {
            var result = await _iShopBiz.GetAllProductsInInventory(pageSize,pageNumber);
            return Ok(result);
        }
        /// <summary>
        /// Creates new Product
        /// </summary>
        /// <param name="addProductModel">Product Add request</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] AddProductModel addProductModel)
        {
            if (ModelState.IsValid)
            {
                await _iShopBiz.AddProductToInventory(addProductModel);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        /// <summary>
        /// Edits a Product
        /// </summary>
        /// <param name="editProductModel"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<IActionResult> EditProduct([FromBody] EditProductModel editProductModel)
        {
            if (ModelState.IsValid)
            {
                await _iShopBiz.EditProductInInventory(editProductModel);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        /// <summary>
        /// Delete a Product from Inventory
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _iShopBiz.DeleteProduct(id);
            return Ok();
        }

    }
}
