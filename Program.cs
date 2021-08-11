using DutchTreat.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace DutchTreat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            if (args.Length > 0 && args[0] == "seed")
            {
                var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
                using (var scope = scopeFactory.CreateScope()) {
                    var seeder = scope.ServiceProvider.GetService<DbSeeder>(); ;
                    seeder.SeedAsync().Wait();
                }
                
            }
            else {
                host.Run();
            }
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
