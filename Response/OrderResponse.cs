using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.Response
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public IEnumerable<string> ItemsIds { get; set; }
        public string Currency { get; set; }
    }
}
