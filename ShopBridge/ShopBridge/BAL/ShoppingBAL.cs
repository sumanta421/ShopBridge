using Microsoft.Extensions.Logging;
using ShopBridge.DAL;
using ShopBridge.Model;
using ShopBridge.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ShopBridge.BAL
{
    public class ShoppingBAL : IShopBridgeBAL
    {
        private IProductRepository _productRepository;
        private ILogger<ShoppingBAL> _logger;
        public ShoppingBAL(IProductRepository productRepository,ILogger<ShoppingBAL> logger=null)
        {
            _productRepository = productRepository;
            _logger = logger;
        }
        public async Task AddProductToInventory(AddProductModel addProduct)
        {
            var isValidated = ValidateProduct(addProduct);

            if (!isValidated)
                throw new ShopBridgeException("Validatation Failed for price",404);
            var existingProduct = await _productRepository.GetItemByName(addProduct.Name);
            if (existingProduct != null)
                throw new ShopBridgeException(ShopBridgeErrorCode.ProductAlreadyExists, 409);
            await _productRepository.AddProductToInventory(addProduct);
        }

        public async Task DeleteProduct(int id)
        {
            var existingProduct = await _productRepository.GetItemById(id);
            if (existingProduct == null)
                throw new ShopBridgeException(ShopBridgeErrorCode.IdNotExists, 404);
            await _productRepository.DeleteProduct(id);
        }

        public async Task EditProductInInventory(EditProductModel editProduct)
        {
            var isValidated = ValidateProduct(editProduct);
            if (!isValidated)
                throw new ShopBridgeException("Validatation Failed for price",400);
            var existingProduct = await _productRepository.GetItemById(editProduct.Id);
            if (existingProduct == null)
                throw new ShopBridgeException(ShopBridgeErrorCode.IdNotExists, 404);
            await _productRepository.EditProductInInventory(editProduct);
        }

        public async Task<IEnumerable<ProductItem>> GetAllProductsInInventory(int? pageSize = null, int? pageNumber = null)
        {
            var result = await _productRepository.GetAllProductsInInventory(pageSize,pageNumber);
            return result;
        }
        private bool ValidateProduct(dynamic product)
        {
            if (product.Price < 0)
                return false;
            return true;
        }
    }
}
