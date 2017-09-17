using RentIt.Project.Models;

namespace RentIt.Project.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<Order> Orders { get; }
        IRepository<Product> Products { get; }
        IRepository<Category> Categories { get; }
        IRepository<Request> Requests { get; }
        IRepository<Filter> Filters { get; }
        IRepository<Characteristic> Characteristics { get; }
        void Save();
    }
}
