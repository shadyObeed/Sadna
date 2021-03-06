using System.Collections.Generic;
using Version1.domainLayer;
using Version1.Service_Layer;
using Version1.domainLayer.UserRoles;
using NUnit.Framework;
using Project_tests;
using Version1.domainLayer.DataStructures;

namespace Project_Tests.AcceptanceTests
{
    public class Uc42GetAddUpdatePaymentInfo:ATProject
    {
        private static SystemAdmin admin;

        private static string initialPolicy;
        private static string storeName;
        private List<string> info;
        private List<string> emptyList;

        private string userName = "user";
        //private static string selling pol
        [SetUp]
        public void Setup()
        {
            admin = new SystemAdmin();
            admin.InitSystem();
            
            //user = new User("user", "userPass");
            Register("user","userPass");
            UserLogin("user","userPass");
            
            initialPolicy = "10% sales";
            storeName = "helloMarket";
            emptyList = new List<string>();
            
            OpenStore(userName, storeName,initialPolicy);
            info = new List<string>();
        }

        [Test]
        public void TestGet()
        {
            //check get after init the store policy in the constructor
            Assert.Equals(emptyList, getPaymentInfo(userName,storeName));
        }
        
        [Test]
        public void TestAdd()
        {
            string newInfo = "newInfo";
            addPaymentInfo(userName, storeName, newInfo);
            
            // happy
            Assert.True(getPaymentInfo(userName,storeName).Contains(newInfo));
            //bad
            Assert.False(getPaymentInfo(userName,storeName).Contains(""));
        }
        [Test]
        public void TestUpdate()
        {
            string newInfo = "secend new info";
            List<string> newinfo = new List<string>();
            newinfo.Add(newInfo);
            updatePaymentInfo(userName, storeName, newinfo);
            
            //happy
            Assert.True(getPaymentInfo(userName,storeName).Contains(newInfo));
            
            //bad the old info is not removed
            Assert.False(getPaymentInfo(userName,storeName).Contains("newInfo"));
        }
    }
}