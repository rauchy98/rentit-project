using System.Collections.Generic;

    public interface IRepository<T> where T : class
    {
        void Create(T item);

        void Update(T item);

        void Delete(int id);

        T Get(int id);

        List<T> GetList();
    }