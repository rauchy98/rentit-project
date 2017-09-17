using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentIt.Project.Models;

namespace RentIt.Project.Repositories
{
    public class CharacteristicRepository : IRepository<Characteristic>
    {
        private IDbContext _context;

        public CharacteristicRepository(IDbContext context)
        {
            _context = context;
        }

        public void Create(Characteristic item)
        {
            _context.Characteristics.Add(item);

        }

        public void Delete(int id)
        {
            var dbItem = _context.Characteristics.FirstOrDefault(x => x.Id == id);
            if(dbItem != null)
            {
                _context.Characteristics.Remove(dbItem);
            }
            else
            {
                throw new Exception("Entity is null");
            }
        }

        public Characteristic Get(int id)
        {
            var dbItem = _context.Characteristics.FirstOrDefault(x => x.Id == id);
            if (dbItem != null)
            {
                return dbItem;
            }
            else
            {
                throw new Exception("Entity is null");
            }
        }

        public List<Characteristic> GetList()
        {
            return _context.Characteristics.ToList();
        }

        public void Update(Characteristic item)
        {
            _context.MarkAsModified(item);
        }
    }
}