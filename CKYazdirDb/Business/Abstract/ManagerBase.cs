using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ODM.CKYazdirDb.DAL;

namespace ODM.CKYazdirDb
{
    public abstract class ManagerBase<T> : IDataAccess<T> where T : class
    {
        private readonly Repository<T> repo = new Repository<T>();

        public virtual int Delete(T obj)
        {
            return repo.Delete(obj);
        }

        public virtual T Find(Expression<Func<T, bool>> where)
        {
            return repo.Find(where);
        }

        public virtual int Insert(T obj)
        {
            return repo.Insert(obj);
        }
        /// <summary>
        /// Tümünü listeler.
        /// </summary>
        /// <returns></returns>
        public virtual List<T> List()
        {
            return repo.List();
        }
        /// <summary>
        /// Tümünü listeler
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual List<T> List(Expression<Func<T, bool>> where)
        {
            return repo.List(where);
        }

        public virtual IQueryable<T> ListQueryable()
        {
            return repo.ListQueryable();
        }

        public virtual int Save()
        {
            return repo.Save();
        }

        public virtual int Update(T obj)
        {
            return repo.Update(obj);
        }
        
        
    }
}
