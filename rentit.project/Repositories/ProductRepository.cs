using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentIt.Project.Models;

namespace RentIt.Project.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private IDbContext _context;
        public ProductRepository(IDbContext context)
        {
            _context = context;
        }
        public void Create(Product product)
        {
            var dbProduct = _context.Products.FirstOrDefault(x => x.Title == product.Title);
            if(dbProduct != null)
            {
                //todo exception
                throw new Exception("Entity already exist");
            }
            _context.Products.Add(product);
        }

        public void Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                //todo exception
                throw new Exception("Entity is null");
            }
            _context.Products.Remove(product);
        }
        public Product Get(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                //todo exception
                throw new Exception("Entity is null");
            }
            return product;
        }
        public List<Product> GetList()
        {
            return _context.Products.ToList();
        }
        public void Update(Product item)
        {
            _context.MarkAsModified(item);
        }
    }
}