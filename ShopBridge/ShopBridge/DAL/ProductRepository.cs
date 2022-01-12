using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShopBridge.Model;
using ShopBridge.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.DAL
{
    [ExcludeFromCodeCoverage]
    public class ProductRepository : IProductRepository
    {
        private string _sqlConnectionString;
        private ILogger<ProductRepository> _ilogger;
        public ProductRepository(IOptions<EnvSetting> envsettings, ILogger<ProductRepository> logger)
        {
            _sqlConnectionString = envsettings.Value.sqlConnectionString;
            _ilogger = logger;
        }
        public async Task AddProductToInventory(AddProductModel addProduct)
        {
            try
            {
                using (var conn = new SqlConnection(_sqlConnectionString))
                {
                    var input = new DynamicParameters();
                    input.Add("@Name", addProduct.Name);
                    input.Add("@Price", addProduct.Price);
                    input.Add("@Description", addProduct.Description);
                    var result = await conn.ExecuteAsync(DALConstants.AddOrEditProduct, commandType: System.Data.CommandType.StoredProcedure, param: input).ConfigureAwait(false);
                };
            }
            catch (Exception ex)
            {
                _ilogger.LogError($"DAL Error -> Add Product : {ex.Message}");
                throw;
            }
        }

        public async Task DeleteProduct(int id)
        {
            try
            {
                using (var conn = new SqlConnection(_sqlConnectionString))
                {
                    var input = new DynamicParameters();
                    input.Add("@Id", id);
                    var result = await conn.ExecuteAsync(DALConstants.DeleteProduct, commandType: System.Data.CommandType.StoredProcedure, param: input).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError($"DAL Error -> Delete Product : {ex.Message}");
                throw;
            }
        }

        public async Task EditProductInInventory(EditProductModel addProduct)
        {
            try
            {
                using (var conn = new SqlConnection(_sqlConnectionString))
                {
                    var input = new DynamicParameters();
                    input.Add("@Id", addProduct.Id);
                    if (addProduct.Name!=null)
                        input.Add("@Name", addProduct.Name);
                    if (addProduct.Price != null)
                        input.Add("@Price", addProduct.Price);
                    if (addProduct.Description != null)
                        input.Add("@Description", addProduct.Description);
                    var result = await conn.ExecuteAsync(DALConstants.AddOrEditProduct, commandType: System.Data.CommandType.StoredProcedure, param: input).ConfigureAwait(false);
                };
            }
            catch (Exception ex)
            {
                _ilogger.LogError($"DAL Error -> Add Product : {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<ProductItem>> GetAllProductsInInventory(int? pageSize = null, int? pageNumber = null)
        {
            try
            {
                using (var conn = new SqlConnection(_sqlConnectionString))
                {
                    var input = new DynamicParameters();
                    if(pageNumber.HasValue || pageSize.HasValue)
                    {
                        input.Add("@PageSize", pageSize.Value);
                        input.Add("@PageNumber", pageNumber.Value);
                    }
                    var result = await conn.QueryAsync<ProductItem>(DALConstants.GetAllProducts, commandType: System.Data.CommandType.StoredProcedure,param:input).ConfigureAwait(false);
                    return result;
                };
            }
            catch (Exception ex)
            {
                _ilogger.LogError($"DAL Error -> Get Product : {ex.Message}");
                throw;
            }
        }

        public async Task<ProductItem> GetItemById(int id)
        {
            try
            {
                using (var conn = new SqlConnection(_sqlConnectionString))
                {
                    var query = string.Format(DALConstants.GetProductById, id);
                    var result = await conn.QueryAsync<ProductItem>(query, commandType: System.Data.CommandType.Text);
                    return result.FirstOrDefault();
                };
            }
            catch (Exception ex)
            {
                _ilogger.LogError($"DAL Error -> Get Product : {ex.Message}");
                throw;
            }
        }

        public async Task<ProductItem> GetItemByName(string name)
        {
            try
            {
                using (var conn = new SqlConnection(_sqlConnectionString))
                {
                    var query = string.Format(DALConstants.GetProductByName, name);
                    var result = await conn.QueryAsync<ProductItem>(query, commandType: System.Data.CommandType.Text);
                    return result.FirstOrDefault();
                };
            }
            catch (Exception ex)
            {
                _ilogger.LogError($"DAL Error -> Get Product : {ex.Message}");
                throw;
            }
        }
    }
}
