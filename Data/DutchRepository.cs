using DutchTreat.Data.Entities;
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

        public IEnumerable<Product> GetAllProducts()
        {
            return dbCtx.Products.OrderBy(p => p.Category).ToList();

        }
    }
}
