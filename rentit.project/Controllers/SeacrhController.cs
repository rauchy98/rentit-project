using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RentIt.Project.Models;
using RentIt.Project.Repositories;

namespace RentIt.Project.Controllers
{
    [RoutePrefix("api/Search")]
    public class SeacrhController : ApiController
    {

        private readonly IUnitOfWork _db;
        public SeacrhController()
        {
            _db = new UnitOfWork(new IDbContext());
        }

        public SeacrhController(IUnitOfWork db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("{query}")]
        public IHttpActionResult Search(string query)
        {
            var result = new
            {
                Categories = new List<CategoryDTO>(),
                Products = new List<ProductDTO>()
            };
            var categories = _db.Categories.GetList().Where(x => x.Title.ToLower().Contains(query.ToLower()));
            var prodects = _db.Products.GetList().Where(x => x.Title.ToLower().Contains(query.ToLower()));
            foreach (var category in categories)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<Category, CategoryDTO>());
                var categoryDTO = Mapper.Map<CategoryDTO>(category);
                result.Categories.Add(categoryDTO);
            }
            foreach (var product in prodects)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<Product, ProductDTO>());
                var productDTO = Mapper.Map<ProductDTO>(product);
                result.Products.Add(productDTO);
            }
            return Ok(result);
        }

    }
}
