using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PPPK.DAL.Implementations.Entity
{
    public class EntityRepository<T> : IRepository<T> where T: class
    {
        private DbContext context;
        public EntityRepository(DbContext context)
        {
            this.context = context;
        }

        public long Add(T entity)
        {
            var entry = context.Entry(entity);
            if(entry.State == EntityState.Detached)
            {
                context.Set<T>().Add(entity);
                entry.State = EntityState.Added;
            }
            return 1;
        }

        public int Delete(long id)
        {
            var entry = context.Entry(GetById(id));
            if (entry.State == EntityState.Detached)
            {
                entry.State = EntityState.Deleted;
            }
            return 1;
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetById(long id)
        {
            throw new NotImplementedException();
        }

        public int Update(T newEntity)
        {
            var entry = context.Entry(newEntity);
            if (entry.State == EntityState.Detached)
            {
                context.Set<T>().Add(newEntity);
                entry.State = EntityState.Modified;
            }
            return 1;
        }
    }
}