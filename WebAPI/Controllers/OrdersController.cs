using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DAL.Entities;
using WebAPI.DTOs;
using System.Net;
using System.Net.Http;
using System.Data.Entity.Infrastructure;

namespace WebAPI.Controllers
{
     [RoutePrefix("api/Orders")]
    public class OrdersController : ApiController
    {
        private dividataEntities db = new dividataEntities();
        private static readonly Expression<Func<Orders, OrdersDTO>> orders = x => new OrdersDTO
        {
            EmployeeID=x.EmployeeID,
            OrderDate=x.OrderDate,
            CompanyName=x.Customers.CompanyName,
            Address=x.Customers.Address,
            CustomerID=x.Customers.CustomerID,
            OrderID=x.OrderID,
            ShipName=x.ShipName,
            Title=x.Employees.Title
        };

       //GET: api/Orders
        public IQueryable<OrdersDTO> GetOrders()
        {
            return db.Orders.Include(b => b.Customers).Select(orders);
        }

        //public IQueryable<Orders> GetOrders() 
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //    return db.Orders;
        //}

        // GET: api/Orders/5
        [Route("{customerid}")]
        public IQueryable<OrdersDTO> GetOrdersByCustomerId(string customerid)
        {
            return db.Orders.Include(b => b.Customers)
                .Where(b => b.Customers.CustomerID.Equals(customerid, StringComparison.OrdinalIgnoreCase))
                .Select(orders);
        }

        //[Route("{id:int}")]
        //[ResponseType(typeof(OrdersDTO))]
        //public IQueryable<OrdersDTO> GetOrdersByEmployeId(int id)
        //{
        //    return db.Orders.Include(b => b.Customers)
        //        .Where(b => b.EmployeeID == id)
        //        .Select(orders);
        //}


        [Route("{id:int}")]
        [ResponseType(typeof(Orders))]
        public IQueryable<OrdersDTO> GetOrdersByOrderID(int id)
        {
             return db.Orders.Include(b => b.Customers)
               .Where(b => b.OrderID == id)
                 .Select(orders);
        }


        // [Route("{id:int}")]
        // [ResponseType(typeof(Orders))]
        //public IHttpActionResult GetOrders(int id)
        //{
        //    Orders orders = db.Orders.Find(id);
        //    if (orders == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(orders);
        //}

        // PUT: api/Orders/5

        [Route("{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrders(int id, Orders orders)
        {
            if (!ModelState.IsValid)
            {
                
                return BadRequest(ModelState);
            }

            if (id != orders.OrderID)
            {
                return BadRequest();
            }

            db.Entry(orders).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Orders

        [ResponseType(typeof(Orders))]
        public IHttpActionResult PostOrders(Orders orders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Orders.Add(orders);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = orders.OrderID }, orders);
        }

        // DELETE: api/Orders/5
        [Route("{id}")]
        [ResponseType(typeof(Orders))]
        public IHttpActionResult DeleteOrders(int id)
        {
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return NotFound();
            }

            db.Orders.Remove(orders);
            db.SaveChanges();

            return Ok(orders);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrdersExists(int id)
        {
            return db.Orders.Count(e => e.OrderID == id) > 0;
        }
    }
}