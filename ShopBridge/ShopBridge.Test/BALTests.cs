using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShopBridge.BAL;
using ShopBridge.DAL;
using ShopBridge.Model;
using ShopBridge.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Test
{
    [TestClass]
    public class BALTests
    {
        [TestMethod]
        public async Task Add_Validation_Failed_Price()
        {
            var mockDAL = new Mock<IProductRepository>();
            mockDAL.Setup(x => x.AddProductToInventory(It.IsAny<AddProductModel>()));
            var inp = new AddProductModel
            {
                Name = "asdsaddsadsa",
                Description = "ddssdfsdfdfs",
                Price = -10
            };
            mockDAL.Setup(x => x.GetItemByName(It.IsAny<string>())).ReturnsAsync(new ProductItem { Name = "Existing" });
            var bal = new ShoppingBAL(mockDAL.Object);
            await Assert.ThrowsExceptionAsync<ShopBridgeException>(async () =>
            { await bal.AddProductToInventory(inp); }
            );

        }
        [TestMethod]
        public async Task Add_Validation_Failed_Existing()
        {
            var mockDAL = new Mock<IProductRepository>();
            mockDAL.Setup(x => x.AddProductToInventory(It.IsAny<AddProductModel>()));
            var inp = new AddProductModel
            {
                Name = "asdsaddsadsa",
                Description = "ddssdfsdfdfs",
                Price = 10
            };
            mockDAL.Setup(x => x.GetItemByName(It.IsAny<string>())).ReturnsAsync(new ProductItem { Name = "Existing" });
            var bal = new ShoppingBAL(mockDAL.Object);
            await Assert.ThrowsExceptionAsync<ShopBridgeException>(async () =>
            { await bal.AddProductToInventory(inp); }
            );

        }

        [TestMethod]
        public async Task Add_Validation_Passed_NoExisting()
        {
            var mockDAL = new Mock<IProductRepository>();
            mockDAL.Setup(x => x.AddProductToInventory(It.IsAny<AddProductModel>()));
            var inp = new AddProductModel
            {
                Name = "asdsaddsadsa",
                Description = "ddssdfsdfdfs",
                Price = 10
            };
            mockDAL.Setup(x => x.GetItemByName(It.IsAny<string>()));
            var bal = new ShoppingBAL(mockDAL.Object);
            await bal.AddProductToInventory(inp);
            mockDAL.Verify(x => x.AddProductToInventory(inp), Times.Once);
        }

        [TestMethod]
        public async Task List_Validation()
        {
            var mockDAL = new Mock<IProductRepository>();
            var list = new List<ProductItem>() as IEnumerable<ProductItem>;
            int? pageNo = null, pageSize = null;
            mockDAL.Setup(x => x.GetAllProductsInInventory(pageSize,pageNo)).ReturnsAsync(list);
            var bal = new ShoppingBAL(mockDAL.Object);
            await bal.GetAllProductsInInventory();
            mockDAL.Verify(x => x.GetAllProductsInInventory(pageSize,pageNo), Times.Once);
        }

        [TestMethod]
        public async Task Delete_Validation_Fail()
        {
            var mockDAL = new Mock<IProductRepository>();
            var list = new List<ProductItem>() as IEnumerable<ProductItem>;
            mockDAL.Setup(x => x.DeleteProduct(It.IsAny<int>()));
            mockDAL.Setup(x => x.GetItemById(It.IsAny<int>()));
            var bal = new ShoppingBAL(mockDAL.Object);
            
            await Assert.ThrowsExceptionAsync<ShopBridgeException>(async () =>
            { await bal.DeleteProduct(-1); }
            );
        }

        [TestMethod]
        public async Task Delete_Validation_Pass()
        {
            var mockDAL = new Mock<IProductRepository>();
            var list = new List<ProductItem>() as IEnumerable<ProductItem>;
            mockDAL.Setup(x => x.DeleteProduct(It.IsAny<int>()));
            mockDAL.Setup(x => x.GetItemById(It.IsAny<int>())).ReturnsAsync(new ProductItem { Name="X"});
            var bal = new ShoppingBAL(mockDAL.Object);
            await bal.DeleteProduct(1);
            mockDAL.Verify(mock => mock.DeleteProduct(1), Times.Once);
        }

        [TestMethod]
        public async Task Edit_Validation_Failed_Existing()
        {
            var mockDAL = new Mock<IProductRepository>();
            mockDAL.Setup(x => x.EditProductInInventory(It.IsAny<EditProductModel>()));
            var inp = new EditProductModel
            {
                Id=1,
                Name = "asdsaddsadsa",
                Description = "ddssdfsdfdfs",
                Price = 10
            };
            mockDAL.Setup(x => x.GetItemById(It.IsAny<int>()));
            var bal = new ShoppingBAL(mockDAL.Object);
            await Assert.ThrowsExceptionAsync<ShopBridgeException>(async () =>
            { await bal.EditProductInInventory(inp); }
            );

        }

        [TestMethod]
        public async Task Edit_Validation_Passed_NoExisting()
        {
            var mockDAL = new Mock<IProductRepository>();
            mockDAL.Setup(x => x.EditProductInInventory(It.IsAny<EditProductModel>()));
            var inp = new EditProductModel
            {
                Id=1,
                Name = "asdsaddsadsa",
                Description = "ddssdfsdfdfs",
                Price = 10
            };
            mockDAL.Setup(x => x.GetItemById(It.IsAny<int>())).ReturnsAsync(new ProductItem { Name="X"});
            var bal = new ShoppingBAL(mockDAL.Object);
            await bal.EditProductInInventory(inp);
            mockDAL.Verify(x => x.EditProductInInventory(inp), Times.Once);
        }
        [TestMethod]
        public async Task Edit_Validation_Failed_Price()
        {
            var mockDAL = new Mock<IProductRepository>();
            mockDAL.Setup(x => x.EditProductInInventory(It.IsAny<EditProductModel>()));
            var inp = new EditProductModel
            {
                Id = 1,
                Name = "asdsaddsadsa",
                Description = "ddssdfsdfdfs",
                Price = -10
            };
            mockDAL.Setup(x => x.GetItemById(It.IsAny<int>())).ReturnsAsync(new ProductItem { Name = "X" });
            var bal = new ShoppingBAL(mockDAL.Object);
            await Assert.ThrowsExceptionAsync<ShopBridgeException>(async () =>
            { await bal.EditProductInInventory(inp); }
            );
        }
    }
}
