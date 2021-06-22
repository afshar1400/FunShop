using FunShop.Core.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunShop.Core.Infrastructrue
{
    public interface ISessionManager
    {
        void AddToSessions(ItemCart item);
        void cleanCart();
        IEnumerable<ItemCart> GetCartItem();
        void RemoveFromSessions(ItemCart item);
    }
}
