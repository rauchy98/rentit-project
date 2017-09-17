using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentIt.Project.Models;

namespace RentIt.Project.Repositories
{
    public class FilterRepository : IRepository<Filter>
    {
        private IDbContext _context;

        public FilterRepository(IDbContext context)
        {
            _context = context;
        }

        public void Create(Filter item)
        {
            _context.Filters.Add(item);

        }

        public void Delete(int id)
        {
            var dbItem = _context.Filters.FirstOrDefault(x => x.Id == id);
            if (dbItem != null)
            {
                _context.Filters.Remove(dbItem);
            }
            else
            {
                throw new Exception("Entity is null");
            }
        }

        public Filter Get(int id)
        {
            var dbItem = _context.Filters.FirstOrDefault(x => x.Id == id);
            if (dbItem != null)
            {
                return dbItem;
            }
            else
            {
                throw new Exception("Entity is null");
            }
        }

        public List<Filter> GetList()
        {
            return _context.Filters.ToList();
        }

        public void Update(Filter item)
        {
            _context.MarkAsModified(item);
        }
    }
}