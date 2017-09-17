using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentIt.Project.Models;

namespace RentIt.Project.Repositories
{
    public class CommentRepository : IRepository<Comment>
    {
        private IDbContext _context;
        public CommentRepository(IDbContext context)
        {
            _context = context;
        }

        public void Create(Comment item)
        {
            _context.Comments.Add(item);
        }

        public void Delete(int id)
        {
            var item = _context.Comments.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _context.Comments.Remove(item);
            }
            else
            {
                throw new Exception("Entity not found");
            }
        }

        public Comment Get(int id)
        {
            var item = _context.Comments.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                return item;
            }
            else
            {
                throw new Exception("Entity not found");
            }
        }

        public List<Comment> GetList()
        {
            return _context.Comments.ToList();
        }

        public void Update(Comment item)
        {
            _context.MarkAsModified(item);
        }
    }
}