using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunShop.Core.Infrastructrue
{
    public interface IZarinPalManager
    {
        Task<string> SendToBank(int amount, int orderId);
        Task<bool> ValidateBank(string authority, int amount);
    }
}
