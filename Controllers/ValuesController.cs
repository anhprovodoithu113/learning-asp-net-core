using Microsoft.AspNetCore.Mvc;
using SampleAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValuesController : Controller
    {
        // Inject the service interface into the constructor
        private IPaymentService _paymentService { get; set; }
        public ValuesController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        public string Get()
        {
            return _paymentService.GetMessage();
        }
    }
}
