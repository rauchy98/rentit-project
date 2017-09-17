using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace RentIt.Project.Models
{
    public interface IBlogDbContext
    {
        IDbSet<Order> Orders { get; set; }

        IDbSet<Product> Products { get; set; }

        IDbSet<User> Users { get; set; }

        IDbSet<Category> Categories { get; set; }

        IDbSet<Comment> Comments { get; set; }

        IDbSet<Request> Requests { get; set; }

        IDbSet<Filter> Filters { get; set; }

        IDbSet<Characteristic> Characteristics { get; set; }

        void MarkAsModified(object item);
    }

    public class IDbContext :  IdentityDbContext<User, ApplicationRole, int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>, IBlogDbContext
    {
        public IDbSet<Order> Orders { get; set; }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<Category> Categories { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<Request> Requests { get; set; }

        public IDbSet<Filter> Filters { get; set; }

        public IDbSet<Characteristic> Characteristics { get; set; }

        public IDbContext() : base("BlogConnection")
        {
        }
        public static IDbContext Create()
        {
            return new IDbContext();
        }

        public void MarkAsModified(object item)
        {
            Entry(item).State = EntityState.Modified;
        }
    }
}