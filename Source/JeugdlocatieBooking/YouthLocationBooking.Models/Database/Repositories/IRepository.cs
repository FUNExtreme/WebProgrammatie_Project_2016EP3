using System.Collections.Generic;

namespace YouthLocationBooking.Data.Database.Repositories
{
    public interface IRepository<T>
    {
        IList<T> GetAll();
        T Get(int id);

        void Insert(T entity);
        void Update(T entity);

        void Remove(T entity);
    }
}
