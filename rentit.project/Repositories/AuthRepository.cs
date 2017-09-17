using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using RentIt.Project.Services;
using System.Security.Claims;
using RentIt.Project.Models;

namespace RentIt.Project.Repositories
{
    public class AuthRepository : IDisposable
    {
        private IDbContext _ctx;

        private UserManager<User, int> _userManager;

        public AuthRepository()
        {
            _ctx = new IDbContext();
            _userManager = new UserManager<User, int>(new ApplicatonUserStore(_ctx));
        }

        public async Task<IdentityResult> RegisterUser(RegisterModel userModel)
        {
            
            User user = new User
            {
                UserName = userModel.UserName,
                Email = userModel.Email,
                Name = userModel.Name
            };
            var result = await _userManager.CreateAsync(user, userModel.Password);
            return result;
        }

        public async Task<User> FindUser(string userName, string password)
        {
            User user = await _userManager.FindAsync(userName, password);

            return user;
        }
        public async Task<User> FindByName(string userName)
        {
            User user = await _userManager.FindByNameAsync(userName);
            return user;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
    }
}