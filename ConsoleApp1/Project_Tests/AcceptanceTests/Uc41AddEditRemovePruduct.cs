using System;
using System.Collections.Generic;
using System.Linq;
using Version1.domainLayer;
using Version1.Service_Layer;
using Version1.domainLayer.UserRoles;
using NUnit.Framework;
using Project_tests;
using Version1.domainLayer.DataStructures;

namespace Project_Tests.AcceptanceTests
{
    public class Uc41AddEditRemovePruduct:ATProject
    {

        private static SystemAdmin admin;
        private string productName;
        private Product product;
        private Product product2;
        private int amount;
        private string storeName;
        private string UserName;
        
        
        [SetUp]
        public void Setup()
        {
            admin = new SystemAdmin();
            admin.InitSystem();
            UserName = "Owneruser";
            //user = new User("user", "userPass");
            storeName = "helloWorldMarket2";
            Register(UserName,"userPass");
            UserLogin(UserName,"userPass");
            OpenStore(UserName,storeName,"" );
            productName = "shampoo";
            product = new Product("shamposo",productName,"12",65,new List<string>());
            product2 = new Product("pringlses",productName,"12",99,new List<string>());
            amount = 100;
            addNewProductToTheSystemAndAddItToShop(storeName, product.Barcode, amount, 9.99, product.Name, "",
                new[] {"fashio","work"});
        }

        [Test]
        public void TestAdd()
        {

            //happy
            Assert.True(getProductsFromShop(UserName, storeName).ContainsKey(product.Barcode));
            //bad
            //todo
            addProductsToShop(UserName, storeName, product.Barcode, 50);
            Assert.True(getProductsFromShop(UserName, storeName).ContainsKey(product.Barcode));
        }
        //[Test]
        public void TestUpdate()
        {
            
            //happy
            UpdateProductAmountInStore(UserName, storeName, product.Barcode, amount - 1);
            int newAmount = (amount - 1);
            Assert.True(getProductsFromShop(UserName,storeName).ContainsKey(product.Barcode) && getProductsFromShop(UserName,storeName)[product.Barcode] == newAmount) ;
            //bad
            newAmount = (amount - 5);
            UpdateProductAmountInStore("user", storeName, product2.Barcode,amount - 5 );
            foreach (var VARIABLE in getProductsFromShop(UserName,storeName))
            {
                if (VARIABLE.Key == productName)
                {
                    Assert.False(VARIABLE.Value == (amount));
                }
            }
            
        }
        //[Test]
        public void TestRemove()
        {
            
            //happy
            Assert.True(RemoveProductFromStore( UserName, storeName, product.Barcode));
            
            //bad
            Assert.False(RemoveProductFromStore( UserName, storeName, product2.Barcode));
        }
    }
}