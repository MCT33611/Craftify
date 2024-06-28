using Craftify.Application.Common.Interfaces.Service;
using Craftify.Infrastructure.Authentication;
using Microsoft.Extensions.Options;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Infrastructure.Services
{
    public class RazorpayService : IRazorpayService
    {
        private readonly RazorpayClient _razorpayClient;

        public RazorpayService(IOptions<RazorpaySettings> razorpaySettings)
        {
            var settings = razorpaySettings.Value;
            _razorpayClient = new RazorpayClient(settings.KeyId, settings.KeySecret);
        }

        public async Task<bool> ValidatePaymentIdAsync(string paymentId)
        {
            try
            {
                await Task.CompletedTask;

                Payment payment = _razorpayClient.Payment.Fetch(paymentId);
                return payment["status"].ToString() == "captured";
            }
            catch (Exception)
            {
                // Log the exception
                return false;
            }
        }

        public async Task<string> CreateOrderIdAsync(decimal? amount)
        {
            await Task.CompletedTask;
            var options = new Dictionary<string, object>
            {
                { "amount", amount! * 100 },  // Amount in smallest currency unit
                { "currency", "INR" },
                { "receipt", "order_rcptid_11_"+amount},
                { "payment_capture", 1 }
            };

            try
            {
                Order order = _razorpayClient.Order.Create(options);
                return order["id"].ToString();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Order creation failed", ex);
            }
        }

    }
}
