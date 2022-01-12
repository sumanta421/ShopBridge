using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.DAL
{
    [ExcludeFromCodeCoverage]
    public class DALConstants
    {
        public const string AddOrEditProduct = "AddOrEditProduct";
        public const string GetAllProducts = "GetAllProducts";
        public const string DeleteProduct = "DeleteProduct";
        public const string GetProductById = "Select * from [dbo].Product where Id={0}";
        public const string GetProductByName = "Select * from [dbo].Product where Name=\'{0}\'"; 
    }
}
