using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Common.Interfaces.Service
{
    public interface IRazorpayService
    {
        Task<bool> ValidatePaymentIdAsync(string paymentId);
        Task<string> CreateOrderIdAsync(decimal? amount);
    }

}
