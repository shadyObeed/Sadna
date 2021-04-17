using System.Collections;
using System.Collections.Generic;
using ConsoleApp1.domainLayer.Business_Layer;
using ConsoleApp1.domainLayer.DataAccessLayer;

namespace ConsoleApp1
{
    public interface GenInterface
    {
        bool InitiateSystem();
        bool GuestLogin();
        bool GuestLogout(User guest);
        bool Register(string userName, string password);
        User MemberLogin(string name, string pass);
        bool MemberLogout(User member);
        Store OpenStore(User manager, string policy, string name);
        Store GetStoreInfo(User user, string name);
        bool CheckStoreInventory(Store store, Hashtable products);
        bool AddProductToStore(User manager, Store store, Product product, int amount);
        List<Product> SearchFilter(User user, string sortOption, List<string> filters);
        bool AddProductToCart(User user, Store store, Product product);
        List<Product> GetCartByStore(User user, Store store);
    }

    public class Gen : GenInterface
    {

        public bool AddProductToStore(User manager, Store store, Product product, int amount)
        {
            store.addProduct(product, amount);
            return true;
        }

        public bool CheckStoreInventory(Store store, Hashtable products)
        {
            throw new System.NotImplementedException();
        }

        public Store GetStoreInfo(User user, string name)
        {
          return DataHandler.Instance.GetStoreinfo(name);
        }

        public bool GuestLogin()
        {
            Guest g1 = new Guest();
            g1.login();
            return g1.signin;
        }

        public bool GuestLogout(User guest)
        {
            throw new System.NotImplementedException();
        }

        public bool InitiateSystem()
        {
            throw new System.NotImplementedException();
        }

        public User MemberLogin(string name, string pass)
        {
           return DataHandler.Instance.loginuser(name, pass);
        }

        public bool MemberLogout(User member)
        {
            throw new System.NotImplementedException();
        }

        public Store OpenStore(User manager, string policy, string name)
        {
            return manager.OpenStore(policy, name);
        }

        public bool Register(string userName, string password)
        {
            if (DataHandler.Instance.exist(userName))
            {
                return false;
            }
            DataHandler.Instance.register(userName, password);
            return true;
        }

        public List<Product> SearchFilter(User user, string sortOption, List<string> filters)
        {
            throw new System.NotImplementedException();
        }
    }
}