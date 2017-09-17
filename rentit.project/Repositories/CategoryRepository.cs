using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentIt.Project.Models;

namespace RentIt.Project.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private IDbContext _context;
        public CategoryRepository(IDbContext context)
        {
            _context = context;
        }

        public void Create(Category item)
        {
            if(_context.Categories.FirstOrDefault(x => x.Title == item.Title) == null)
            {
                _context.Categories.Add(item);
            }
            else
            {
                throw new Exception("Entity already exist");
            }
        }

        public void Delete(int id)
        {
            var item = _context.Categories.FirstOrDefault(x => x.Id == id);
            if(item != null)
            {
                _context.Categories.Remove(item);
            }
            else
            {
                throw new Exception("Entity not found");
            }
        }

        public Category Get(int id)
        {
            var item = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                return item;
            }
            else
            {
                throw new Exception("Entity not found");
            }
        }

        public List<Category> GetList()
        {
            return _context.Categories.ToList();
        }

        public void Update(Category item)
        {
            _context.MarkAsModified(item);
        }
    }
}