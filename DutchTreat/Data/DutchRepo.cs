using DutchTreat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DutchTreat.Data
{
    public class DutchRepo : IDutchRepo
    {
        private readonly DutchContext _ctx;

        public DutchRepo(DutchContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _ctx.Products
                .OrderBy(p => p.Title)
                .ToList();
        }

        public IEnumerable<Product> GetAllProductsByCat(string category)
        {
            return _ctx.Products
                .Where(p => p.Category == category)
                .ToList();
        }

        public bool Save()
        {
            return _ctx.SaveChanges() > 0;
        }

        public IEnumerable<Order> GetAllOrder(bool includeItems)
        {
            if(includeItems)
            {
                return _ctx.Orders
                        .Include(x => x.Items)
                        .ThenInclude(i => i.Product)
                        .ToList();
            }
            else
            {
                return _ctx.Orders
                        .ToList();
            }
            
        }

        public Order GetOrderById(string name , int id)
        {
            return _ctx.Orders
                .Include(x => x.Items)
                .ThenInclude(i => i.Product)
                .Where(o => o.Id == id && o.Users.UserName == name)
                .FirstOrDefault();
        }

        public void addEntity(object model)
        {
            _ctx.Add(model);
        }

        public IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems)
        {
            if (includeItems)
            {
                return _ctx.Orders
                        .Where(o => o.Users.UserName == username)
                        .Include(x => x.Items)
                        .ThenInclude(i => i.Product)
                        .ToList();
            }
            else
            {
                return _ctx.Orders
                        .Where(o => o.Users.UserName == username)
                        .ToList();
            }
        }
    }
}
