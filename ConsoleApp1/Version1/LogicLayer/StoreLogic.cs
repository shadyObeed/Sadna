using System.Collections.Generic;
using System.Linq;
using Version1.DataAccessLayer;
using Version1.domainLayer;
using Version1.domainLayer.DataStructures;
using Version1.domainLayer.StorePolicies;

namespace Version1.LogicLayer
{
    public static class StoreLogic
    {

        public static bool OpenStore(string managerName, string storeName, string policy)
        {
            if (!DataHandler.Instance.Exists(managerName)) return false;
            var store = new Store(managerName, storeName);
            return DataHandler.Instance.AddStore(store);
        }

        public static  bool AddProductToStore(string storeName, string barcode, int amount)
        {
            var product = DataHandler.Instance.GetProduct(barcode);
            var store = DataHandler.Instance.GetStore(storeName);
            if (product == null || store == null) return false;
            
            store.addProduct(barcode, amount);
            
            // add to data access
            
            return true;

        }

        public static bool IsManger(string storeName, string mangerName)
        {
            if (!DataHandler.Instance.Stores.ContainsKey(storeName) ||
                !DataHandler.Instance.Stores[storeName].GetManagers().Keys.Contains(mangerName))
                return false;
            return true;
        }
        
        public static bool IsOwner(string storeName, string ownerName)
        {
            if (!DataHandler.Instance.Stores.ContainsKey(storeName) ||
                !DataHandler.Instance.Stores[storeName].GetOwners().Keys.Contains(ownerName))
                return false;
            return true;
        }

        public static bool UpdateProductAmountInStore(string userName, string storeName, string productBarcode, int amount)
        {
            lock (DataHandler.Instance.InefficientLock)
            {
                var user = DataHandler.Instance.GetUser(userName);
                var store = DataHandler.Instance.GetStore(storeName);
                var product = DataHandler.Instance.GetProduct(productBarcode);

                if (user == null || store == null || product == null) return false;
                
                var inventory = store.GetInventory();
                if (!inventory.ContainsKey(productBarcode)) return false;
                inventory[productBarcode]= amount;
                return true;
            }
        }


        public static bool RemoveProductFromStore(string userName, string storeName, string productBarcode)
        {
            lock (DataHandler.Instance.InefficientLock)
            {
                var user = DataHandler.Instance.GetUser(userName);
                var store = DataHandler.Instance.GetStore(storeName);
                var product = DataHandler.Instance.GetProduct(productBarcode);

                if (user == null || store == null || product == null) return false;

                var inventory = store.GetInventory();
                return inventory.ContainsKey(productBarcode) && inventory.TryRemove(productBarcode, out _);
            }
        }

        public static List<string> GetStoreOwners(string storeName)
        {
            var store = DataHandler.Instance.GetStore(storeName);
            return store?.GetOwners().Keys.ToList();
        }
        
        public static List<string> GetStoreManagers(string storeName)
        {
            var store = DataHandler.Instance.GetStore(storeName);
            return store?.GetManagers().Keys.ToList();
        }

        
        
        public static bool AddOwner(string storeName, string apointerid, string apointeeid)
        {
            var appointerUser = DataHandler.Instance.GetUser(apointerid); 
            var newOwner = DataHandler.Instance.GetUser(apointeeid);
            var store = DataHandler.Instance.GetStore(storeName);
            if (appointerUser == null || newOwner == null || store == null ) return false;
            if (!IsOwner(storeName,apointerid) || IsOwner(storeName, apointeeid)) return false;
            store.GetOwners().Add(apointeeid, new List<string>());
            store.GetOwners()[apointerid].Add(apointeeid);
            return true;
        }
        
        public static bool AddManager(string storeName, string apointerid, string apointeeid, int permissions)
        {
            var appointerUser = DataHandler.Instance.GetUser(apointerid);
            var newManager = DataHandler.Instance.GetUser(apointeeid);
            var store = DataHandler.Instance.GetStore(storeName);
            if (appointerUser == null || newManager == null || store == null) return false;
            if (IsManger(storeName, apointeeid) || !IsValidPermission(permissions)) return false;
            store.GetManagers().Add(apointeeid , permissions);
            return true;
        }
        
        public static bool RemoveOwner(string storeName, string username)
        {
            var owner = DataHandler.Instance.GetUser(username);
            var store = DataHandler.Instance.GetStore(storeName);
            if (owner == null || store == null) return false;
            
            return store.GetOwners().Remove(username); // returns false if the owner was not found
        }
        
        public static bool RemoveManager(string storeName, string username)
        {
            var manager = DataHandler.Instance.GetUser(username);
            var store = DataHandler.Instance.GetStore(storeName);
            if (manager == null || store == null) return false;
            Dictionary<string, int> mangers = store.GetManagers();
            return mangers.Remove(username); // returns false if the manager was not found
        }

        private static bool IsValidPermission(int permissions)
        {
            return Permissions.IsValidPermission(permissions);
        }
        
        
//---------------------------------------- Getters ----------------------------------------//   

        public static List<Store> GetAllStores()
        {
            return DataHandler.Instance.Stores.Values.ToList();
        }

        public static List<Store> GetUserStores(string userName)
        {
            var stores = new List<Store>();
            if (DataHandler.Instance.GetUser(userName) == null)
                return null;
            
            foreach (var store in GetAllStores())
            {
                if(IsOwner(store.GetName(),userName) || IsManger(store.GetName(),userName))
                    stores.Add(store);
            }

            return stores;
        }


        public static string GetStorePolicy(string storeName)
        {
            var store = DataHandler.Instance.GetStore(storeName);
            return store?.GetPurchasePolicies().ToString();
        }

        public static List<string> GetStoresNames()
        {
            return DataHandler.Instance.Stores.Keys.ToList();
        }
        
        public static List<string> GetStorePurchaseHistory(string ownerUser, string storeName)
        {
            var store = DataHandler.Instance.GetStore(storeName);
            if (store == null) return null;
            var history = new List<string>();

            foreach (var purchase in store.GetHistory())
            {
                history.Add(purchase.ToString());
            }

            return history;
        }

        public static bool UpdateStorePolicy(string storeName, IPurchasePolicy newPolicy)
        {
            var store = DataHandler.Instance.GetStore(storeName);
            if (store == null) return false;
            
            store.GetPurchasePolicies().Add(newPolicy);

            return true;

        }
        
    }
}