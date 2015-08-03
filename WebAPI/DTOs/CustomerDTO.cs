using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.DTOs
{
    public class CustomerDTO
    {
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
    }
}