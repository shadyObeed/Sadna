﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.domainLayer.Business_Layer
{
    class User : Person
    {
        private string username;
        private string password;
        private ShoppingCart cart;
        private List<KeyValuePair<Purchase, Store>> history;
        public User(string username,string password)
        {
            this.username = username;
            this.password = password;
            cart = new ShoppingCart(username);
            history = new List<KeyValuePair<Purchase, Store>>();
        }
        public User(string username, string password,ShoppingCart cart)
        {
            this.username = username;
            this.password = password;
            this.cart = cart;
        }
        public string UserName { get => username; }
        public string Password { get => password; }
        public ShoppingCart Cart { get => cart;  }
        public void AddItemToBasket(string store_name,Product pr, int amount)
        {
            for (int i = 0; i < cart.baskets.Count; i++)
            {
                if (Cart.baskets[i].Storename.CompareTo(store_name) == 0)
                {
                    cart.baskets[i].addproduct(pr, amount);
                }
            }
        }

        public void EditBasket()
        {
            throw new NotImplementedException();
        }

        public string GetBasketInfo(string store_name)
        {
            for (int i = 0; i < cart.baskets.Count; i++)
            {
                if (Cart.baskets[i].Storename.CompareTo(store_name) == 0)
                {
                    return cart.baskets[i].ToString();
                }
            }
            return "didnt find absket for this store";
        }



      

        public void Login()
        {
            throw new NotImplementedException();
        }




 

        public void logout()
        {
            throw new NotImplementedException();
        }

        public Store OpenStore(string sellpol,string name)
        {
            Store newstore = new Store(this, sellpol, name);
            return newstore;
        }

        public void WriteReview()
        {
            throw new NotImplementedException();
        }

        public void RateProductAndStore()
        {
            throw new NotImplementedException();
        }

        public void SendMessageToStore()
        {
            throw new NotImplementedException();
        }

        public void SendComplain()
        {
            throw new NotImplementedException();
        }

        public string GetPersonalPurchaseHistory()
        {
            string output = "history of "+UserName ;
            for (int i = 0; i < history.Count; i++)
            {
                output += "\n"+history[i].Key.ToString()+" from the "+history[i].Value.Name +"store";
            }
            return output;
        }

   
    }
}