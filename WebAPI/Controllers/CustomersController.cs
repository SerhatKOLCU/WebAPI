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
    public class CustomersController : ApiController
    {
        private dividataEntities db = new dividataEntities();
        private static readonly Expression<Func<Customers,CustomerDTO>> ascustomer=
            x=>new CustomerDTO{
            CompanyName=x.CompanyName,
            Address=x.Address,
            City=x.City            
        };
       //  GET: api/Customers
        //public IQueryable<Customers> GetCustomers()
        //{
        //    db.Configuration.ProxyCreationEnabled = false;//tablolar arası ilişkiyi parçalamak için
        //    return db.Customers;
        //}
        public IQueryable<CustomerDTO> GetCustomers()
        {
           // db.Configuration.ProxyCreationEnabled = false;//tablolar arası ilişkiyi parçalamak için
            return db.Customers.Include(b => b.Orders).Select(ascustomer);
        }

        public IQueryable<Customers> GetPersonsByCarId(int Id)
        {
            IQueryable<Customers> Persons = db.Customers.Include(p => p.Orders.Where(c => c.OrderID == Id)
            .Select(c => c.OrderID));
            return Persons;
        }

        // GET: api/Customers/5
        [ResponseType(typeof(Customers))]
        public IHttpActionResult GetCustomer(string id)
        {
            Customers customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }
        //[ResponseType(typeof(string))]
        //public string GetCustomer(string id)
        //{
        //    Customers customer = db.Customers.Find(id);
        //    if (customer == null)
        //    {
        //        return "bulunamadııı";
        //    }

        //    return "bulundu mevcut";
        //    //return Ok(customer);
        //}

        // PUT: api/Customers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomer(string id, Customers customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustomerID)
            {
                return BadRequest();
            }

            db.Entry(customer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customers
        [ResponseType(typeof(Customers))]
        public IHttpActionResult PostCustomer(Customers customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Customers.Add(customer);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.CustomerID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = customer.CustomerID }, customer);
        }

        // DELETE: api/Customers/5
        [ResponseType(typeof(Customers))]
        public IHttpActionResult DeleteCustomer(string id)
        {
            Customers customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            db.Customers.Remove(customer);
            db.SaveChanges();

            return Ok(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(string id)
        {
            return db.Customers.Count(e => e.CustomerID == id) > 0;
        }
    }
}