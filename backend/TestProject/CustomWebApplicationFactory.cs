using System;
using System.Linq;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TestProject
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
    where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // find dbcontext
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase("testdb");
                });

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<AppDbContext>();
                db.Database.EnsureCreated();
                var company = new Company()
                {
                    Name = Guid.NewGuid().ToString("n")[..8]
                };
                db.Companies.Add(company);
                db.Games.Add(new Game()
                {
                    DevCompany = company,
                    PubCompany = company,
                    Name = "TestGame",
                    ReleaseDate = DateTime.Now
                });
                db.SaveChanges();
            });
        }
    }
}