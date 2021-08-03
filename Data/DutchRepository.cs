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
        public IEnumerable<Product> GetAllProducts()
        {
            var result = new List<Product>();
            result.Add(new Product
            {
                Id = 100,
                Title = "Title 1"
            });
            result.Add(new Product
            {
                Id = 200,
                Title = "Title 2"
            });
            return result;

        }
    }
}
