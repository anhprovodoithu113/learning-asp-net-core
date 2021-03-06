using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.Requests
{
    public class OrderRequest
    {
        [Required]
        public IEnumerable<string> ItemsIds { get; set; }
        [Required]
        [Currency]
        public string Currency { get; set; }
    }
}
