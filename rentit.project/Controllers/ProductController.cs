using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using RentIt.Project.Models;
using RentIt.Project.Repositories;

namespace RentIt.Project.Controllers
{
    [RoutePrefix("api/Product")]
    public class ProductController : ApiController
    {
        IUnitOfWork _db;
        public ProductController()
        {
            _db = new UnitOfWork(new IDbContext());
        }
        public ProductController(IUnitOfWork db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            Product product;
            try
            {
                product = _db.Products.Get(id);
            }
            catch
            {
                return NotFound();
            }
            Mapper.Initialize(cfg => cfg.CreateMap<Product, ProductDTO>());
            var productDTO = Mapper.Map<ProductDTO>(product);
            productDTO.SellerId = product.Seller.Id;
            productDTO.RequestCount = product.Requests.Count;

            return Ok(productDTO);
        }

        [HttpGet]
        [Route("{id}/CheckRequest")]
        [Authorize]
        public IHttpActionResult CheckRequest(int id)
        {
            Product product;
            try
            {
                product = _db.Products.Get(id);
            }
            catch
            {
                return NotFound();
            }
            User user;
            try
            {
                user = _db.Users.Get(CurrentUserId());
            }
            catch
            {
                return NotFound();
            }
            if(user.Requests.FirstOrDefault(x => x.Product.Id == product.Id) == null)
            {
                return Ok(false);
            }
            return Ok(true);
        }

        [Authorize]
        [HttpPut]
        [Route("{id}/Request")]
        public IHttpActionResult Reqest(int id)
        {
            Product product;
            try
            {
                product = _db.Products.Get(id);
            }
            catch
            {
                return NotFound();
            }
            User user;
            try
            {
                user = _db.Users.Get(CurrentUserId());
            }
            catch
            {
                return NotFound();
            }
            var request = new Request()
            {
                User = user,
                Product = product
            };
            if (user.Requests.FirstOrDefault(x => x.Product.Id == product.Id) == null)
            {
                _db.Requests.Create(request);
                _db.Save();
            }
            return Ok();
        }

        [HttpGet]
        [Route("Dates/{productId}")]
        public IHttpActionResult GetProductDates(int productId)
        {
            Product product;
            try
            {
                product = _db.Products.Get(productId);
            }
            catch
            {
                return NotFound();
            }
            var dates = product.Oredrs.ToDictionary(x => x.Start, x => x.End);
            return Ok(dates);
        }

        [HttpGet]
        [Route("Category/{categoryId}")]
        public IHttpActionResult GetProducrtByCategory(int categoryId)
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
            foreach (var sub in category.Subcategories)
            {
                list.AddRange(sub.Products);
            }
            var productsDTO = new List<ProductDTO>();
            foreach (var item in list)
            {
                var productDTO = Mapper.Map<ProductDTO>(item);
                productDTO.SellerId = item.Seller.Id;
                productDTO.RequestCount = item.Requests.Count;
                productsDTO.Add(productDTO);

            }
            return Ok(productsDTO);
        }

        [Authorize]
        [HttpPost]
        [Route("Category/{categotyId}")]
        public IHttpActionResult Post(int categotyId, [FromBody] ProductDTO productDTO)
        {
            if (!ModelState.IsValid) return NotFound();
            Mapper.Initialize(cfg => cfg.CreateMap<ProductDTO, Product>());
            var product = Mapper.Map<Product>(productDTO);
            Category category;
            try
            {
                category = _db.Categories.Get(categotyId);
            } 
            catch
            {
                return NotFound();
            }
            if (category.Subcategories.Count != 0)
            {
                return BadRequest();
            }
            var userid = CurrentUserId();
            User user;
            try
            {
                user = _db.Users.Get(userid);
            }
            catch
            {
                return NotFound();
            }
            product.Seller = user;
            category.Products.Add(product);
            _db.Save();
            var addedProduct = category.Products.Last();
            Mapper.Initialize(cfg => cfg.CreateMap<Product,ProductDTO>());
            var addedProductDTO = Mapper.Map<ProductDTO>(addedProduct);
            

            return Ok(addedProductDTO);
        }

   

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Put(int id, [FromBody] ProductDTO productDTO)
        {
            //check if admin
           
            Product product;
            try
            {
                product = _db.Products.Get(id);
            }
            catch
            {
                return NotFound();
            }
            product.Title = productDTO.Title ?? product.Title;
            product.Price = productDTO.Price == 0? product.Price : productDTO.Price;
            product.Description = productDTO.Description ?? product.Description;
            //product.Picture = productDTO.Picture ?? product.Picture;
            product.Available = productDTO.Available;
            _db.Products.Update(product);
            _db.Save();
            return Ok();
        }

        [HttpGet]
        [Route("{productId}/Comments")]
        public IHttpActionResult GetComments(int productId)
        {
            Product product;
            try
            {
                product = _db.Products.Get(productId);
            }
            catch
            {
                return NotFound();
            }
            var commentsDTO = new List<CommentDTO>();
            foreach (var comment in product.Comments)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<Comment, CommentDTO>());
                var commentDTO = Mapper.Map<CommentDTO>(comment);
                commentDTO.AuthorId = comment.Author.Id;
                commentDTO.ProductId = comment.Product.Id;
                commentsDTO.Add(commentDTO);
            }
            return Ok(commentsDTO);
        }

        [Authorize]
        [HttpPost]
        [Route("{productId}/Comments")]
        public IHttpActionResult AddComment(int productId, CommentDTO commentDTO)
        {
            Product product;
            try
            {
                product = _db.Products.Get(productId);
            }
            catch
            {
                return NotFound();
            }
            Mapper.Initialize(cfg => cfg.CreateMap<CommentDTO, Comment>());
            var comment = Mapper.Map<Comment>(commentDTO);
            comment.Product = product;
            var userId = CurrentUserId();
            User user; 
            try
            {
                user = _db.Users.Get(userId);
            }
            catch
            {
                return NotFound();
            }
            comment.Author = user;
            product.Comments.Add(comment);
            _db.Save();
            return Ok();
        }

        private int CurrentUserId()
        {
            var a = (ClaimsIdentity)User.Identity;
            int userId = Convert.ToInt32(a.Claims.First().Value);
            return userId;
        }
    }
}
