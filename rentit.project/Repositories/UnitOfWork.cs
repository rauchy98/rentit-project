using System;
using RentIt.Project.Models;

namespace RentIt.Project.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContext _context;
        private IRepository<User> _userRepository;
        private IRepository<Category> _categoryRepository;
        private IRepository<Order> _orderRepository;
        private IRepository<Product> _productRepository;
        private IRepository<Request> _requestsRepository;
        private IRepository<Filter> _filtersRepository;
        private IRepository<Characteristic> _characteristicsRepository;

        public UnitOfWork(IDbContext context)
        {
            _context = context;
        }

        public IRepository<Category> Categories
        {
            get
            {
                if (_categoryRepository == null)
                {
                    _categoryRepository = new CategoryRepository(_context);
                }
                return _categoryRepository;
            }
        }

        public IRepository<Order> Orders
        {
            get
            {
                if (_orderRepository == null)
                {
                    _orderRepository = new OrderRepository(_context);
                }
                return _orderRepository;
            }
        }

        public IRepository<Product> Products
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(_context);
                }
                return _productRepository;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_context);
                }
                return _userRepository;
            }
        }

        public IRepository<Request> Requests
        {
            get
            {
                if (_requestsRepository == null)
                {
                    _requestsRepository = new RequestRepository(_context);
                }
                return _requestsRepository;
            }
        }

        public IRepository<Filter> Filters
        {
            get
            {
                if (_filtersRepository == null)
                {
                    _filtersRepository = new FilterRepository(_context);
                }
                return _filtersRepository;
            }
        }

        public IRepository<Characteristic> Characteristics
        {
            get
            {
                if (_characteristicsRepository == null)
                {
                    _characteristicsRepository = new CharacteristicRepository(_context);
                }
                return _characteristicsRepository;
            }
        }



        public void Save()
        {
            _context.SaveChanges();
        }
    }
}