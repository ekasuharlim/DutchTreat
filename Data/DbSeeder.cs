using DutchTreat.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;

namespace DutchTreat.Data
{
    public class DbSeeder
    {
        private readonly DutchContext ctx;
        private readonly IWebHostEnvironment env;

        public DbSeeder(DutchContext ctx, IWebHostEnvironment env) {
            this.ctx = ctx;
            this.env = env;
        }

        public void Seed() {
            var jsonfile = Path.Combine(env.ContentRootPath , "Data/art.json");
            var jsondata = File.ReadAllText(jsonfile);
            var products = JsonSerializer.Deserialize <IEnumerable<Product>>(jsondata);
            if (!ctx.Products.Any()) {
                ctx.AddRange(products);
            }

            var order = new Order
            {
                OrderDate = DateTime.Now,
                OrderNumber = "1000",
                Items = new List<OrderItem> {
                    new OrderItem{
                        Product = products.FirstOrDefault(),
                        Quantity = 10,
                        UnitPrice = 2000
                    }
                }
            };

            ctx.Orders.Add(order);
            
            ctx.SaveChanges();
        }
    }
}
