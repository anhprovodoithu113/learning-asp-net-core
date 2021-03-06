using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.Services
{
    public interface IPaymentService
    {
        string GetMessage();
    }

    public class PaymentService : IPaymentService
    {
        public string GetMessage() => "Pay my money.";
    }

    public class ExternalPaymentService : IPaymentService
    {
        public string GetMessage() => "Pay my money from external service.";
    }
}
