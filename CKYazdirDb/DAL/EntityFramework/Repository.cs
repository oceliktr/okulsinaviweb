using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ODM.CKYazdirDb.DAL
{
    public class Repository<T> : RepositoryBase, IRepository<T> where T:class 
    {
        private readonly DbSet<T> _objectSet;
        public Repository()
        {
            _objectSet = context.Set<T>();
        }
        public List<T> List()
        {
            return _objectSet.ToList();
        }
        public Task<List<T>> ListAsync()
        {
            return _objectSet.ToListAsync();
        }
        public List<T> List(Expression<Func<T, bool>> where)
        {
            return _objectSet.Where(where).ToList();
        }

        public IQueryable<T> ListQueryable()
        {
            return _objectSet.AsQueryable<T>();
        }

        public int Insert(T obj)
        {
            _objectSet.Add(obj);
            return Save();
        }
        public int Update(T obj)
        {
            return Save();
        }
        public int Delete(T obj)
        {
            _objectSet.Remove(obj);
            return Save();
        }

        public void DeleteAll(string obj)
        {
            //Tablo içinde yer alan satırları hızlı bir şekilde silmek için TRUNCATE deyimi kullanılır. 
            context.Database.ExecuteSqlCommand("Delete From "+obj);
        }
        public T Find(Expression<Func<T, bool>> where)
        {
            return _objectSet.FirstOrDefault(where);
        }
        public int Save()
        {
            return context.SaveChanges();
        }

        
    }
}
