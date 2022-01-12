using ShopBridge.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.DAL
{
    public interface IProductRepository
    {
        /// <summary>
        /// Add Product to database
        /// </summary>
        /// <param name="addProduct"></param>
        /// <returns></returns>
        Task AddProductToInventory(AddProductModel addProduct);
        /// <summary>
        /// Edit PRoduct in Database
        /// </summary>
        /// <param name="editProduct"></param>
        /// <returns></returns>
        Task EditProductInInventory(EditProductModel editProduct);
        /// <summary>
        /// Gets all products from Database
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        Task<IEnumerable<ProductItem>> GetAllProductsInInventory(int? pageSize = null, int? pageNumber = null);
        /// <summary>
        /// Deletes Product From Database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteProduct(int id);
        /// <summary>
        /// Gets Product by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ProductItem> GetItemById(int id);
        /// <summary>
        /// Gets Product By Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<ProductItem> GetItemByName(string name);
    }
}
