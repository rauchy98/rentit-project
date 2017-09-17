using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RentIt.Project.Models;
using RentIt.Project.Repositories;

namespace RentIt.Project.Controllers
{
    [RoutePrefix("api/Category")]
    public class CategoryController : ApiController
    {
        IUnitOfWork _db;
        public CategoryController()
        {
            _db = new UnitOfWork(new IDbContext());
        }
        public CategoryController(IUnitOfWork db)
        {
            _db = db;
        }
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetList()
        {
            var categories = _db.Categories.GetList().Where(x => x.Subcategories.Count != 0);
            Mapper.Initialize(cfg => cfg.CreateMap<Category, CategoryDTO>());
            List<CategoryDTO> categiriesDTO = new List<CategoryDTO>();
            foreach (var category in categories) {
                var categoryDTO = new CategoryDTO
                {
                    Id = category.Id,
                    Title = category.Title,
                    Icon = category.Icon,
                };
                categoryDTO.Subcategories = category.Subcategories.Select(item => new CategoryDTO {
                    Id = item.Id,
                    Title = item.Title,
                    Icon = item.Icon
                }).ToList();
                categiriesDTO.Add(categoryDTO);
            }
            return Ok(categiriesDTO);
        }
            
        [Route("{categoryId}")]
        [HttpGet]
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
            Mapper.Initialize(cfg => cfg.CreateMap<Product, ProductDTO>());
            List<Product> list = category.Products.ToList();
            foreach(var sub in category.Subcategories)
            {
                list.AddRange(sub.Products);
            }
            var products = new List<ProductDTO>();
            foreach (var item in list)
            {
                var productDTO = Mapper.Map<ProductDTO>(item);
                productDTO.SellerId = item.Seller.Id;
                productDTO.RequestCount = item.Requests.Count;
                products.Add(productDTO);

            }
            return Ok(new { category.Title, category.Icon, products });
        }
        [Route("")]
        [HttpPost]
        public IHttpActionResult PostCategory(CategoryDTO categotyDTO)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<CategoryDTO, Category>());
            var categoty = Mapper.Map<Category>(categotyDTO);
            if (ModelState.IsValid)
            {
                _db.Categories.Create(categoty);
                _db.Save();
                return Ok();
            }
            else return NotFound();
        }
        [Route("{categotyId}")]
        [HttpPut]
        public IHttpActionResult PutCategory(int categotyId, ProductDTO productDTO)
        {
            try
            {
                if (!ModelState.IsValid) return NotFound();
                Mapper.Initialize(cfg => cfg.CreateMap<ProductDTO, Product>());
                var product = Mapper.Map<Product>(productDTO);
                Category category;
                try
                {
                    category = _db.Categories.Get(categotyId); // todo add cathing exception
                }
                catch
                {
                    return NotFound();
                }
                if(category.Subcategories.Count != 0)
                {
                    return BadRequest();
                }
                category.Products.Add(product);
                _db.Save();
            }
            catch
            {
                return NotFound();
            }
            return Ok();
        }
        
        [HttpGet]
        [Route("{categoryId}/Subcategory")]
        public IHttpActionResult GetSubcategories(int categoryId)
        {
            Category category = null;
            try
            {
                //todo test name uniqe
                category = _db.Categories.Get(categoryId);
            }
            catch
            {
                return NotFound();
            }
            Mapper.Initialize(cfg => cfg.CreateMap<Category, CategoryDTO>());
            List<Category> list = category.Subcategories.ToList();
            var categoriesDTO = new List<CategoryDTO>();
            foreach (var item in list)
            {
                categoriesDTO.Add(Mapper.Map<CategoryDTO>(item));
            }
            return Ok(categoriesDTO);
        } 

        [HttpPost]
        [Route("{categoryId}/Subcategory")]
        public IHttpActionResult AddSubcategory(int categoryId, CategoryDTO categoryDTO)
        {
            Category category = null;
            try
            {
                //todo test name uniqe
                category = _db.Categories.Get(categoryId);
            }
            catch
            {
                return NotFound();
            }
            if (category.Categoty != null)
            {
                return BadRequest();
            }
            Mapper.Initialize(cfg => cfg.CreateMap<CategoryDTO, Category>());
            var subcategoty = Mapper.Map<Category>(categoryDTO);
            category.Subcategories.Add(subcategoty);
            _db.Save();
            return Ok();
            
        }
    }
}
