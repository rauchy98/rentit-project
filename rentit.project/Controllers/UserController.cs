using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using RentIt.Project.Models;
using RentIt.Project.Repositories;

namespace RentIt.Project.Controllers
{
    [RoutePrefix("api/Users")]
    [Authorize]
    public class UserController : ApiController
    {
        private IUnitOfWork _db;
        public UserController()
        {
            _db = new UnitOfWork(new IDbContext());
        }
        public UserController(IUnitOfWork db)
        {
            _db = db;
        }

        [AllowAnonymous]
        [Route("Me")]
        [HttpGet]
        public IHttpActionResult GetCurrentUser()
        {
            User user;
            try
            {
                user = _db.Users.Get(CurrentUserId());
            }
            catch
            {
                return NotFound();
            }
            Mapper.Initialize(cfg => cfg.CreateMap<User, UserDTO>());
            var userDTO = Mapper.Map<UserDTO>(user);
            return Ok(userDTO);
        }

        public IHttpActionResult GetUsers()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<User, UserDTO>());
            List<User> users = _db.Users.GetList().ToList();
            List<UserDTO> usersDTO = new List<UserDTO>();
            foreach(var user in users)
            {
                usersDTO.Add(Mapper.Map<UserDTO>(user));
            }
            return Ok(usersDTO);
        }

        [AllowAnonymous]
        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetUser(int id)
        {
            User user;
            try
            {
                user = _db.Users.Get(id);
            } catch
            {
                return NotFound();
            }
            Mapper.Initialize(cfg => cfg.CreateMap<User, UserDTO>());
            var userDTO = Mapper.Map<UserDTO>(user);
            return Ok(userDTO);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IHttpActionResult DeleteUser(int id)
        {
            try
            {
                _db.Users.Delete(id);
            }
            catch
            {
                return NotFound();
            }
            _db.Save();
            return Ok();
        }
        [HttpPut]
        [Route("")]
        public IHttpActionResult PostUser([FromBody]UserDTO userDTO)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<UserDTO, User> ());
            //User user = Mapper.Map<User>(userDTO);
            var user = _db.Users.Get(CurrentUserId());
            user.Name = userDTO.Name ?? user.Name;
            _db.Users.Update(user);
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
