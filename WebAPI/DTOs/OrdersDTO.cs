using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.DTOs
{
    public class OrderDTO
    {
        public int OrderID { get; set; } 
        public Nullable<int> EmployeeID { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public string CustomerAddress { get; set; }
        public string CompanyName { get; set; }
        public string CustomerID { get; set; }
        public string ShipName { get; set; }
        public string EmployeeTitle { get; set; }
    }
}