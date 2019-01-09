using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace DutchTreat.Data
{
    public class Dutchseeder
    {
        private readonly DutchContext _ctx;
        private readonly IHostingEnvironment _hosting;
        private readonly UserManager<StoreUser> _userManager;

        public Dutchseeder(DutchContext ctx , IHostingEnvironment hosting,UserManager<StoreUser> userManager)
        {
            _ctx = ctx;
            _hosting = hosting;
            _userManager = userManager;
        }
        public async Task Seeder()
        {

            _ctx.Database.EnsureCreated();

            var user = await _userManager.FindByEmailAsync("mohanand.dabool1@gmail.com");
            if (user == null)
            {
                user = new StoreUser()
                {
                    FirstName = "Mo",
                    LastName = "Dabool",
                    UserName = "mohanad.dabool1@gmail.com",
                    Email = "mohanand.dabool1@gmail.com"
                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Failed to create default user");
                }
            }

            if (!_ctx.Products.Any())
            {
                var filePath = Path.Combine(_hosting.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filePath);
                var product = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                _ctx.Products.AddRange(product);

                var order = new Order()
                {
                    OrderNumber = "12345",
                    Users = user,
                    OrderDate = DateTime.Now,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product = product.First(),
                            Quantity = 5,
                            UnitPrice = product.First().Price
                        }
                    }
                };

                _ctx.Orders.Add(order);
                _ctx.SaveChanges();
            }
        }
    }
}
