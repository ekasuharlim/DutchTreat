using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext dbCtx;

        public DutchRepository(DutchContext dbCtx)
        {
            this.dbCtx = dbCtx;
        }

        public void AddEntity(object entity)
        {
            this.dbCtx.Add(entity);
        }

        public IEnumerable<Order> GetAllOrder()
        {
            return dbCtx.Orders.Include( o => o.Items).OrderByDescending(o => o.Id);
        }
        

        public IEnumerable<Product> GetAllProducts()
        {
            return dbCtx.Products.OrderBy(p => p.Category).ToList();

        }


        public Order GetOrder(int id)
        {
            return dbCtx.Orders.Include(o => o.Items).FirstOrDefault(o => o.Id == id);
        }

        public Product GetProduct(int id) {
            return dbCtx.Products.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveAll()
        {
            try
            {
                this.dbCtx.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
