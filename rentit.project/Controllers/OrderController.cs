using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using RentIt.Project.Models;
using RentIt.Project.Repositories;

namespace RentIt.Project.Controllers
{
    [Authorize]
    [RoutePrefix("api/Order")]
    public class OrderController : ApiController
    {
        IUnitOfWork _db;
        public OrderController()
        {
            _db = new UnitOfWork(new IDbContext());
        }
        public OrderController(IUnitOfWork db)
        {
            _db = db;
        }
        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            var id = CurrentUserId();
            User user;
            try
            {
                user = _db.Users.Get(id);
            }
            catch
            {
                return NotFound();
            }
            var orders = user.Orders;
            Mapper.Initialize(cfg => cfg.CreateMap<Order, OrderDTO>());
            var ordersDTO = new List<OrderDTO>();
            foreach (var item in orders)
            {
                var orderDTO = Mapper.Map<OrderDTO>(item);
                orderDTO.ProductId = item.Product.Id;
                ordersDTO.Add(orderDTO);
            }
            return Ok(ordersDTO);
        }

        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var userId = CurrentUserId();
            var order = _db.Orders.Get(id);
            if (userId != order.User.Id) return NotFound();
            Mapper.Initialize(cfg => cfg.CreateMap<Order, OrderDTO>());
            var orderDTO = Mapper.Map<OrderDTO>(order);
            orderDTO.ProductId = order.Product.Id;
            return Ok(orderDTO);
        }
        [Route("Product/{productId}")]
        [HttpPost]
        public IHttpActionResult Put(int productId, [FromBody] OrderDTO orderDTO)
        {
            if (!ModelState.IsValid) return NotFound();
            Mapper.Initialize(cfg => cfg.CreateMap<OrderDTO,Order>());
            
            var order = Mapper.Map<Order>(orderDTO);
            order.Start = new DateTime(order.Start.Year, order.Start.Month, order.Start.Day);
            order.End = new DateTime(order.End.Year, order.End.Month, order.End.Day);
            try
            {
                order.Product = _db.Products.Get(productId);
            }
            catch
            {
                return NotFound();
            }
            var id = CurrentUserId();
            User user;
            try
            {
                user = _db.Users.Get(id);
            }
            catch
            {
                return NotFound();
            }
            order.User = user;
            if(!(DateTime.Compare(order.Start,order.End) < 0))
            {
                return BadRequest();
            }
            var existingOrder = user.Orders.FirstOrDefault(x => x.Product.Id == productId && x.Start == orderDTO.Start);
            if (existingOrder != null)
            {
                if (existingOrder.User.Id == order.User.Id)
                {
                    existingOrder.Amount += orderDTO.Amount;
                    existingOrder.Price = existingOrder.Amount * existingOrder.Product.Price;
                }
                else
                {
                    if (!isValidOrder(order.Product, order.Start, order.End))
                    {
                        return BadRequest("Order not valid");
                    }
                }
            }
            else
            {
                order.Price = order.Amount * order.Product.Price;
                _db.Orders.Create(order);
            }
            _db.Save();
            return Ok();
        }

        private bool isValidOrder(Product product,DateTime start, DateTime end)
        {
            if(DateTime.Compare(start,end) > 0)
            {
                return false;
            }
            var orders = product.Oredrs;
            foreach(var order in orders)
            {
                if(DateTime.Compare(order.Start,start) < 0 && DateTime.Compare(order.End, start) > 0
                    || DateTime.Compare(order.Start, end) < 0 && DateTime.Compare(order.End, end) > 0)
                {
                    return false;
                }
            }
            return true;
        }


        [Route("{orderId}")]
        [HttpDelete]
        public IHttpActionResult Delete(int orderId)
        {
            try
            {
                var order = _db.Orders.Get(orderId);
                if (CurrentUserId() != order.User.Id) return NotFound();
                _db.Orders.Delete(orderId);
                _db.Save();
            } catch
            {
                return NotFound();
            }
            return Ok();
        }

        [Route("{orderId}")]
        [HttpPut]
        public IHttpActionResult Update(int orderId, [FromBody] OrderDTO orderDTO)
        {
            if (!ModelState.IsValid) return NotFound();
            Order order;
            try
            {
                order = _db.Orders.Get(orderId);
            }
            catch
            {
                return NotFound();
            }
            if (CurrentUserId() != order.User.Id) return NotFound();
            order.End = orderDTO.End;
            //order.Start = orderDTO.Start;
            order.Amount = orderDTO.Amount;
            _db.Orders.Update(order);
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
