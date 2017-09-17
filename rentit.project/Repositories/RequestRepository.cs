using System;
using System.Collections.Generic;
using System.Linq;
using RentIt.Project.Models;

namespace RentIt.Project.Repositories
    {
        public class RequestRepository : IRepository<Request>
        {
            private IDbContext _context;
            public RequestRepository(IDbContext context)
            {
                _context = context;
            }

            public void Create(Request item)
            {
                _context.Requests.Add(item);
            }

            public void Delete(int id)
            {
                var item = _context.Requests.FirstOrDefault(x => x.Id == id);
                if (item != null)
                {
                    _context.Requests.Remove(item);
                }
                else
                {
                    throw new Exception("Entity not found");
                }
            }

            public Request Get(int id)
            {
                var item = _context.Requests.FirstOrDefault(x => x.Id == id);
                if (item != null)
                {
                    return item;
                }
                else
                {
                    throw new Exception("Entity not found");
                }
            }

            public List<Request> GetList()
            {
                return _context.Requests.ToList();
            }

            public void Update(Request item)
            {
                _context.MarkAsModified(item);
            }
        }
    }