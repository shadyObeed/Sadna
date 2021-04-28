using System.Collections.Generic;
using Version1.domainLayer;
using Version1.Service_Layer;
using Version1.domainLayer.UserRoles;
using NUnit.Framework;
using Project_tests;
using Version1.domainLayer.DataStructures;

namespace Project_Tests.AcceptanceTests
{
    public class Uc46UpdateMangerResponsibilities:ATProject
    {
        private static SystemAdmin admin;
        private static User user;
        private static Store store;
        string storeName;
        
        [SetUp]
        public void Setup()
        {
            admin = new SystemAdmin();
            initSystem(admin);
            
            //user = new User("user0", "userPass");
            Register("user0","userPass");
            user = loginGuest("user0", "userPass");
            
            Register("user1","user1");
            Register("user2","user2");
            Register("user3", "user3");

            storeName = "ebay";
           
            
            OpenStore(user.UserName,"sellPolicy", storeName);
            store = getUsersStore(user,storeName);
            
                
        }

        [Test]
        public void Test()
        {
            //happy
            string newMangerName = "user1";
            string newRespon = "newResp";
            Assert.True(AddNewManger(user, store, newMangerName));
            Assert.True(IsManger(store, newMangerName));
            List<string> responsibilities = getMangerResponsibilities(user, store,newMangerName);
            responsibilities.Add(newRespon);

            Assert.True(updateMangerResponsibilities(user, storeName, responsibilities));
            Assert.Equals(responsibilities, getMangerResponsibilities(user, store, newMangerName));
            
            
            
            //bad
            responsibilities.Remove(newRespon);
            Assert.AreNotEqual(responsibilities, getMangerResponsibilities(user, store, newMangerName));
            
        }
       
    }
}