using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PPPK.DAL.Implementations.Entity
{
    public class EntityRepository<T> : IRepository<T> where T : class
    {
        private DbContext context;
        private DbSet<T> dbSet;
        public EntityRepository(DbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public long Add(T entity)
        {
            var entry = context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                dbSet.Add(entity);
            }
            return 1;
        }

        public int Delete(long id)
        {
            var entry = context.Entry(dbSet.Find(id));

            entry.State = EntityState.Deleted;
            return 1;
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet;
        }

        public T GetById(long id)
        {
            return dbSet.Find(id);
        }

        public int Update(T newEntity)
        {
            var entry = context.Entry(newEntity);
            if (entry.State == EntityState.Detached)
            {
                dbSet.Add(newEntity);
            }
            entry.State = EntityState.Modified;

            return 1;
        }
    }
}