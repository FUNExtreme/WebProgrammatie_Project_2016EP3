using System.Collections.Generic;

namespace YouthLocationBooking.Data.Database.Repositories
{
    public interface IRepository<TEntity>
    {
        IList<TEntity> GetAll();
        TEntity Get(int id);

        void Attach(TEntity entity);

        void Insert(TEntity entity);
        void Update(TEntity entity);

        void Remove(TEntity entity);
    }
}
