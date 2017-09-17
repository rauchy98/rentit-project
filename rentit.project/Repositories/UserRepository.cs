using System;
using System.Collections.Generic;
using System.Linq;
using RentIt.Project.Models;

namespace RentIt.Project.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly IBlogDbContext _context;
        public UserRepository(IBlogDbContext context)
        {
            _context = context;

        }
        public void Create(User item)
        {
            if (_context.Users.Where(x => x.Id == item.Id) != null)
            {
                throw new Exception("Entity already exist");
            }
            _context.Users.Add(item);
        }
        public void Delete(int id)
        {
            var item = _context.Users.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                //todo exception
                throw new Exception("Entity is null");
            }
            _context.Users.Remove(item);
        }
        public User Get(int id)
        {
            var item = _context.Users.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                //todo exception
                throw new Exception("Entity is null");
            }
            return item;
        }
        public List<User> GetList()
        {
            return _context.Users.ToList();
        }
        public void Update(User item)
        {
            _context.MarkAsModified(item);
        }
    }
}