using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_App.Repo.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Get(long id);
        void Add(TEntity entity);
        void AddRange(IList<TEntity> entities);

        void Update(TEntity entity);
        void Remove(TEntity entity);
        void SaveChanges();
    }
}
