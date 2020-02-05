using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPK.DAL
{
    public interface IRepository<T> where T: class
    {
        IEnumerable<T> GetAll();
        T GetById(long id);
        long Add(T entity);
        int Update(T newEntity);
        int Delete(T entity);
    }
}
