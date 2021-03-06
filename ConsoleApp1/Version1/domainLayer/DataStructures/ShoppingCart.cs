using System.Collections.Generic;

namespace Version1.domainLayer.DataStructures
{
    public class ShoppingCart
    {
        // <storeName,ShoppingBasket>
        internal Dictionary<string, ShoppingBasket> shoppingBaskets { get; set; }

        public ShoppingCart()
        {
            shoppingBaskets = new Dictionary<string, ShoppingBasket>();
        }

        public ShoppingBasket GetBasket(string storeName)
        {
            if (!shoppingBaskets.ContainsKey(storeName))
                return null;
            return shoppingBaskets[storeName];
        }
        public bool AddBasket(ShoppingBasket shoppingBasket)
        {
            if (shoppingBaskets.ContainsKey(shoppingBasket.StoreName))
                return false;
            shoppingBaskets.Add(shoppingBasket.StoreName,shoppingBasket);
            return true;
        }
        public bool RemoveBasket(ShoppingBasket shoppingBasket)
        {
            return shoppingBaskets.Remove(shoppingBasket.StoreName);
        }

        public bool AddProductToBasket(string storeName, string productBarCode, int amount)
        {
            if (!shoppingBaskets.ContainsKey(storeName))
                AddBasket(new ShoppingBasket(storeName));
            return shoppingBaskets[storeName].AddProduct(productBarCode, amount);
        }
    }
}
