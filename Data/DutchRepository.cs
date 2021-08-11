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

        public IEnumerable<Order> GetAllOrder(bool includeOrderItem)
        {
            if (includeOrderItem)
            {
                return dbCtx.Orders
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .OrderByDescending(o => o.Id);

            }
            else {
                return dbCtx.Orders;                        
            }
        }
        public Order GetOrder(int id)
        {
            return dbCtx.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(o => o.Id == id);
        }


        public IEnumerable<Product> GetAllProducts()
        {
            return dbCtx.Products.OrderBy(p => p.Category).Take(10).ToList();

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
