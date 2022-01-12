using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Utils
{

    public enum ShopBridgeErrorCode
    {
        #region Add/Edit Product
        [Description("Product Already Exists")]
        ProductAlreadyExists,
        #endregion

        #region Delete Product
        [Description("Specified Id doesnt exist")]
        IdNotExists,
        #endregion

        #region List Product

        #endregion
    }
}
