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
    [RoutePrefix("api/Filter")]
    public class FilterController : ApiController
    {
        IUnitOfWork _db;

        public FilterController()
        {
            _db = new UnitOfWork(new IDbContext());
        }

        public FilterController(IUnitOfWork db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("Category/{categoryId}")]
        public IHttpActionResult Get(int categoryId)
        {
            Category category = null;
            try
            {
                category = _db.Categories.Get(categoryId);
            }
            catch
            {
                return NotFound();
            }
            var filters = new List<Filter>();
            filters.AddRange(category.Filters);
            if (category.Categoty != null)
            {
                filters.AddRange(category.Categoty.Filters);
            }
            var listDTO = new List<FilterDTO>();
            foreach(var filter in filters)
            {
                listDTO.Add(new FilterDTO
                {
                    Id = filter.Id,
                    Name = filter.Name,
                    Values = filter.Characteristics.Select(x=> x.Value).Distinct().ToList()
                });
            }
            return Ok(listDTO);
        }

        [HttpPost]
        [Route("Category/{categoryId}")]
        public IHttpActionResult Post(int categoryId, FilterDTO filterDTO)
        {
            Category category = null;
            try
            {
                category = _db.Categories.Get(categoryId);
            }
            catch
            {
                return NotFound();
            }
            _db.Filters.Create(new Filter
            {
                Name = filterDTO.Name,
                Category = category
            });
            _db.Save();
            return Ok();
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult GetProductsByFilter([FromBody] IList<FilterValue>filterValues)
        {
            var productsDTOResult = new List<ProductDTO>();
            var filters = new List<Filter>();

            foreach (var filterValue in filterValues) {
                filters.Add(_db.Filters.Get(filterValue.FilterId));
            }
            
            foreach(var filter in filters)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<Product, ProductDTO>());
                var productsDTO = filter.Characteristics
                    .Where(characteristics => filterValues.Where(x=> x.FilterId == filter.Id).ToList().Select(y=> y.Value).Contains(characteristics.Value))
                    .Select(characteristics => characteristics.Product)
                    .Select(product => Mapper.Map<ProductDTO>(product));
                productsDTOResult.AddRange(productsDTO);
            }
            return Ok(productsDTOResult);
            
        }
    }
}
