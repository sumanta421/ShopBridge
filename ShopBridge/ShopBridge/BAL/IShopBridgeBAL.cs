using ShopBridge.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.BAL
{
    public interface IShopBridgeBAL
    {
        /// <summary>
        /// Add Product to Inventory
        /// </summary>
        /// <param name="addProduct"></param>
        /// <returns></returns>
        public Task AddProductToInventory(AddProductModel addProduct);
        /// <summary>
        /// Edit Product in Inventory
        /// </summary>
        /// <param name="editProduct"></param>
        /// <returns></returns>
        public Task EditProductInInventory(EditProductModel editProduct);
        /// <summary>
        /// Gets all products in inventory
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public Task<IEnumerable<ProductItem>> GetAllProductsInInventory(int? pageSize = null, int? pageNumber = null);
        /// <summary>
        /// Deletes Product in Inventory
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task DeleteProduct(int id);
    }
}
