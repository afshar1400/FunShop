using Dto.Payment;
using FunShop.Core.Infrastructrue;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using ZarinPal.Class;
using System.Threading.Tasks;

namespace FunShop.MVC.Infrastracture
{
    public class ZarinPalManager:IZarinPalManager
    {
        private readonly Payment _payment;
        private readonly string MerchantId = "1234b5d4-0048-11e8-94db-005056a205be";
        public ZarinPalManager()
        {
            var expose = new Expose();
            _payment = expose.CreatePayment();
        }

        //for payment requires three step
        //1==>request to getway to get authority
        //2 ==>sending user with callback url and orderID
        //3 => validating payment to check it is actual callback

        public async Task<string> SendToBank(int amount,int orderId)
        {
            //Todo req
            var req = new DtoRequest()
            {
                Mobile = "09338733485",
                CallbackUrl = $"https://localhost:5001/order/Thankyou/{orderId}",
                Description = orderId.ToString(),
                Email = "abolfazlafshar003@gmail.com",
                Amount = amount,
                MerchantId=MerchantId,
                
            };
            var result = await _payment.Request(req, Payment.Mode.sandbox);
            if (result.Status == 100)
            {
               return "https://sandbox.zarinpal.com/pg/StartPay/" + result.Authority;

            }

            return null;
        }

        public async Task<bool> ValidateBank(string authority,int amount)
        {
            var verification = await _payment.Verification(new DtoVerification
            {
                Amount = amount,
                MerchantId = MerchantId,
                Authority = authority,
                
            }, Payment.Mode.sandbox);
            //for success verifaction is 101 status code
            if (verification.Status == 101 | verification.Status==100)
                return true;
            return false;
        }
    }
}
