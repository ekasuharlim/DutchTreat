using DutchTreat.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace DutchTreat.Data
{
    public class DbSeeder
    {
        private readonly DutchContext ctx;
        private readonly IWebHostEnvironment env;
        private readonly UserManager<StoreUser> userManager;

        public DbSeeder(DutchContext ctx, IWebHostEnvironment env, UserManager<StoreUser> userManager)
        {
            this.ctx = ctx;
            this.env = env;
            this.userManager = userManager;
        }

        public async Task SeedAsync() {
            var jsonfile = Path.Combine(env.ContentRootPath , "Data/art.json");
            var jsondata = File.ReadAllText(jsonfile);
            var products = JsonSerializer.Deserialize <IEnumerable<Product>>(jsondata);
            if (!ctx.Products.Any()) {
                ctx.AddRange(products);
            }

            var user = new StoreUser
            {
                UserName = "Eka",
                FirstName = "F1",
                LastName = "L1"
            };
            var userResult = await this.userManager.CreateAsync(user,"P@ssw0rd");
            if (userResult != IdentityResult.Success) 
            {
                throw new ApplicationException("failed create user");   
            }

            var order = new Order
            {
                User = user,
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
