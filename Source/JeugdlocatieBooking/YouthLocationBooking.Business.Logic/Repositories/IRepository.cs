using System;
using System.Collections.Generic;

namespace YouthLocationBooking.Business.Logic.Repositories
{
    public interface IRepository<T> : IDisposable
    {
        IList<T> GetAll();
        T Get(int id);

        void Add(T entity);
        void Update(T entity);

        void Remove(T entity);
    }
}
