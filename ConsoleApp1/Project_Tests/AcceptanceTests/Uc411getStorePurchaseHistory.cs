using System.Collections.Generic;
using Version1.domainLayer;
using Version1.Service_Layer;
using Version1.domainLayer.UserRoles;
using NUnit.Framework;
using Project_tests;
using Version1.domainLayer.DataStructures;

namespace Project_Tests.AcceptanceTests
{
    public class Uc411getStorePurchaseHistory:ATProject
    {
        private static SystemAdmin admin;
        private static User ownerUser;
        string storeName;
        
        [SetUp]
        public void Setup()
        {
            admin = new SystemAdmin();
            admin.InitSystem();
            ownerUser = new User("user0", "userPass");
            Register("user1","user1");
            Register("user2","user2");
            storeName = "aliExpress";
            OpenStore(ownerUser.UserName,storeName,"sellPolicy");
        }

        [Test]
        public void Test()
        {
            UserLogin("user1","user1");
            UserLogin("user2","user2");
            Product product = new Product("shampoo", "des", "15", 655,new List<string>());
            addProductsToShop(ownerUser.UserName, storeName, product.Barcode, 13);

            Assert.True(buyProduct("user1", storeName, product.Barcode, 2));
            Assert.True(buyProduct("user2", storeName, product.Barcode, 3));
            //happy
            Assert.NotNull(getStorePurchaseHistory(ownerUser.UserName, storeName));
        }
       
    }
}