using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace RentIt.Project.Models
{
    public class User : IdentityUser<int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public bool isAdmin { get; set; }
        //public byte[] Picture { get; set; }
        public string Name { get; set; }
        //public DateTime? Birth { get; set; }
        public virtual List<Order> Orders { get; set; }

        public virtual List<Comment> Comments { get; set; }
        public virtual List<Product> Products { get; set; }
        public virtual List<Request> Requests { get; set; }
    }

    public class UserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
    }

    public class ApplicationUserRole : IdentityUserRole<int>
    {
    }
    public class ApplicationUserLogin : IdentityUserLogin<int>
    {
    }
    public class ApplicationUserClaim : IdentityUserClaim<int>
    {
    }
    public class ApplicationRole : IdentityRole<int, ApplicationUserRole>
    {
    }
    public class ApplicatonUserStore :
        UserStore<User, ApplicationRole, int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicatonUserStore(IDbContext context)
            : base(context)
        {
        }
    }

}