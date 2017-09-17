using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentIt.Project.Models;

namespace RentIt.Project.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private IDbContext _context;
        public OrderRepository(IDbContext context)
        {
            _context = context;
        }

        public void Create(Order order)
        {
            _context.Orders.Add(order);
        }

        public void Delete(int id)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                //todo exception
                throw new Exception("Entity is null");
            }
            _context.Orders.Remove(order);
        }
        public Order Get(int id)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                //todo exception
                throw new Exception("Entity is null");
            }
            return order;
        }
        public List<Order> GetList()
        {
            return _context.Orders.ToList();
        }
        public void Update(Order item)
        {
            _context.MarkAsModified(item);
        }
    }
}