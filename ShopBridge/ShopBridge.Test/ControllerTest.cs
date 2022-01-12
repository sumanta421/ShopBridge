using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShopBridge.BAL;
using ShopBridge.Controllers;
using ShopBridge.Model;
using ShopBridge.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopBridge.Test
{
    [TestClass]
    public class ControllerTest
    {
        [TestMethod]
        public async Task Add_Validation_Failed()
        {
            var mockBal = new Mock<IShopBridgeBAL>();
            mockBal.Setup(x => x.AddProductToInventory(It.IsAny<AddProductModel>()));
            var inp = new AddProductModel
            {
                Name = "asdsaddsadsa",
                Description = "ddssdfsdfdfs",
                Price = 10
            };
            var _productController = new ProductController(mockBal.Object);
            var result = await _productController.AddProduct(inp);
            mockBal.Verify(mock => mock.AddProductToInventory(inp), Times.Once);
        }

        [TestMethod]
        public async Task List()
        {
            var mockBal = new Mock<IShopBridgeBAL>();
            var list = new List<ProductItem>() as IEnumerable<ProductItem>;
            mockBal.Setup(x => x.GetAllProductsInInventory(It.IsAny<int?>(), It.IsAny<int?>())).ReturnsAsync(list);
            var _productController = new ProductController(mockBal.Object);
            int? pageNo = null, pageSize = null;
            var result = await _productController.GetAllProducts();
            mockBal.Verify(mock => mock.GetAllProductsInInventory(pageNo,pageSize), Times.Once);
        }

        [TestMethod]
        public async Task Delete()
        {
            var mockBal = new Mock<IShopBridgeBAL>();
            var list = new List<ProductItem>() as IEnumerable<ProductItem>;
            mockBal.Setup(x => x.DeleteProduct(It.IsAny<int>()));
            var _productController = new ProductController(mockBal.Object);
            var result = await _productController.DeleteProduct(0);
            mockBal.Verify(mock => mock.DeleteProduct(0), Times.Once);
        }


        [TestMethod]
        public async Task Edit()
        {
            var mockBal = new Mock<IShopBridgeBAL>();
            var list = new List<ProductItem>() as IEnumerable<ProductItem>;
            var _productController = new ProductController(mockBal.Object);
            mockBal.Setup(x => x.EditProductInInventory(It.IsAny<EditProductModel>()));
            var inp = new EditProductModel
            {
                Id=2,
                Name = "asdsaddsadsa",
                Description = "ddssdfsdfdfs",
                Price = 10
            };
            var result = await _productController.EditProduct(inp);
            mockBal.Verify(mock => mock.EditProductInInventory(inp), Times.Once);
        }
    }
}
