using Challenge_App.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_App.Repo.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly MyContext _context;


        public Repository(MyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }
        public async Task<TEntity> Get(long id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }


        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void AddRange(IList<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }
    }
}
