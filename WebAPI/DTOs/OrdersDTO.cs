using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.DTOs
{
    public class OrdersDTO
    {
        public int OrderID { get; set; } 
        public Nullable<int> EmployeeID { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public string Address { get; set; }
        public string CompanyName { get; set; }
        public string CustomerID { get; set; }
        public string ShipName { get; set; }
        public string Title { get; set; }
    }
}