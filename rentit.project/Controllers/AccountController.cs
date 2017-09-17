using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Http;
using System.Linq;
using System;
using System.Net;
using System.Web.Http.ModelBinding;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using AutoMapper;
using RentIt.Project.Models;
using RentIt.Project.Repositories;

namespace RentIt.Project.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private AuthRepository _repo;

        public AccountController()
        {
            _repo = new AuthRepository();
        }

        public AccountController(AuthRepository repo)
        {
            _repo = repo;
        }

        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _repo.RegisterUser(registerModel);
            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }
            User user = await _repo.FindUser(registerModel.UserName, registerModel.Password);
            //todo: map to dto and return
            Mapper.Initialize(cfg => cfg.CreateMap<User, UserDTO>());
            var userDTO = Mapper.Map<UserDTO>(user);
            return Ok(userDTO);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("CheckLogin/{userName}")]
        public async Task<IHttpActionResult> CheckLogin(string userName)
        {
            User user = await _repo.FindByName(userName);
            if(user == null)
            {
                return NotFound();
            }
            Mapper.Initialize(cfg => cfg.CreateMap<User, UserDTO>());
            var userDTO = Mapper.Map<UserDTO>(user);

            return Ok(userDTO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }
            base.Dispose(disposing);
        }
        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }
            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(nameof(RegisterModel).ToLower() + ".UserName", error);
                    }
                }
                if (ModelState.IsValid)
                {
                    return BadRequest();
                }
                return BadRequest(ModelState);
            }
            return null;
        }
    }
}