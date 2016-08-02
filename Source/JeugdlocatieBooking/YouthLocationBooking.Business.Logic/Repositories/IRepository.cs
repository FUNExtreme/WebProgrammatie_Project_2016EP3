using System;
using System.Collections.Generic;

namespace YouthLocationBooking.Business.Logic.Repositories
{
    public interface IRepository<T> : IDisposable
    {
        IEnumerable<T> GetAll();
        T Get(int id);

        void Add(T entity);

        void Remove(T entity);
    }
}
