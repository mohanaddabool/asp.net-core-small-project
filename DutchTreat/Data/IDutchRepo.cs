using System.Collections.Generic;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Data
{
    public interface IDutchRepo
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetAllProductsByCat(string category);

        IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems);
        Order GetOrderById(string name, int orderId);
        void addEntity(object model);
        bool Save();
        
    }
}