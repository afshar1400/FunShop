using FunShop.Core.Infrastructrue;
using FunShop.Core.services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunShop.MVC.Infrastracture
{
    public class SessionManager :ISessionManager
    {
        private string sessionKey = "cart";
        private ISession _session;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }


        public void AddToSessions(ItemCart item)
        {
            string cart = _session.GetString(sessionKey);
            List<ItemCart> cartList;
            if (string.IsNullOrEmpty(cart))
            {
                 item.Qty = 1;
                 cartList = new List<ItemCart>();
                 cartList.Add(item);
            }
            else
            {
                 cartList = JsonConvert.DeserializeObject<List<ItemCart>>(cart);

                if(cartList.Any(p => p.ProductId == item.ProductId))
                {
                    cartList.Find(x => x.ProductId == item.ProductId).Qty++;
                }
                else
                {
                    item.Qty = 1;
                    cartList.Add(item);
                }
                
            }
            _session.SetString(sessionKey, JsonConvert.SerializeObject(cartList));
        }

        public void RemoveFromSessions(ItemCart item)
        {
            string cart = _session.GetString(sessionKey);
           
            if (!string.IsNullOrEmpty(cart))
            {
            List<ItemCart> cartList = JsonConvert.DeserializeObject<List<ItemCart>>(cart);

               
                if (cartList.Any(x => x.ProductId == item.ProductId))
                {
                    int qty = cartList.Find(x => x.ProductId ==item.ProductId).Qty;
                    if (qty > 1)
                    {
                        cartList.Find(x => x.ProductId == item.ProductId).Qty--;
                    }
                    else if (qty == 1)
                    {
                        cartList = cartList.Where(s => s.ProductId != item.ProductId).ToList();
                    }
                }

            _session.SetString(sessionKey, JsonConvert.SerializeObject(cartList));
            }
        }

        public IEnumerable<ItemCart> GetCartItem()
        {
            string cart = _session.GetString(sessionKey);
            List<ItemCart> cartList = null ;
            if (!string.IsNullOrEmpty(cart))
            {
            cartList = JsonConvert.DeserializeObject<List<ItemCart>>(cart);
            }
            return cartList;
        }

        public void cleanCart()
        {
            _session.Remove(sessionKey);
        }
    }
}

