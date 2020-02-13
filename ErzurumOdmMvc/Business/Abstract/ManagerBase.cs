using ErzurumOdmMvcDAL.Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ErzurumOdmMvc.Business.Abstract
{
    public abstract class ManagerBase<T> : IDataAccess<T> where T : class
    {
        private readonly Repository<T> _repo = new Repository<T>();
        public T Procedure(string procedureName)
        {
            return _repo.Procedure(procedureName);
        }
        public T Procedure(string procedureName, object param)
        {
            return _repo.Procedure(procedureName, param);
        }
        public Task<T> ProcedureAsync(string procedureName, object param)
        {
            return _repo.ProcedureAsync(procedureName, param);
        }
        public object ProcedureScalar(string procedureName, object param)
        {
            return _repo.ProcedureScalar(procedureName, param);
        }
        public object Scalar(string sql)
        {
            return _repo.Scalar(sql);
        }
        public object Scalar(string sql, object param)
        {
            return _repo.Scalar(sql, param);
        }
        public Task<object> ScalarAsync(string sql)
        {
            return _repo.ScalarAsync(sql);
        }
        public Task<object> ScalarAsync(string sql, object param)
        {
            return _repo.ScalarAsync(sql, param);
        }
        public void ProcedureGES(string procedureName, object param)
        {
            _repo.ProcedureGES(procedureName, param);
        }
        public void ProcedureGESAsync(string procedureName, object param)
        {
            _repo.ProcedureGESAsync(procedureName, param);
        }
        public IEnumerable<T> ProcedureList(string procedureName)
        {
            IEnumerable<T> list = _repo.ProcedureList(procedureName);
            return list;
        }
        public IEnumerable<T> ProcedureList(string procedureName, object param)
        {
            IEnumerable<T> list = _repo.ProcedureList(procedureName, param);
            return list;
        }
        public async Task<IEnumerable<T>> ProcedureListAsync(string procedureName, object param)
        {
            IEnumerable<T> list = await _repo.ProcedureListAsync(procedureName, param);
            return list;
        }
        public IEnumerable<T> List()
        {
            return _repo.List();
        }
        public Task<IEnumerable<T>> ListAsync()
        {
            return _repo.ListAsync();
        }
        public long Insert(T obj)
        {
            return _repo.Insert(obj);
        }
        public Task<int> InsertAsync(T obj)
        {
            return _repo.InsertAsync(obj);
        }
        public T Find(int id)
        {
            return _repo.Find(id);
        }
        public Task<T> FindAsync(int id)
        {
            return _repo.FindAsync(id);
        }
        public T Find(string sql)
        {
            return _repo.Find(sql);
        }
        public T Find(string sql, object param)
        {
            return _repo.Find(sql, param);
        }
        public Task<T> FindAsync(string sql)
        {
            return _repo.FindAsync(sql);
        }
        public Task<T> FindAsync(string sql, object param)
        {
            return _repo.FindAsync(sql, param);
        }
        public IEnumerable<T> Query(string sql)
        {
            return _repo.Query(sql);
        }
        public Task<IEnumerable<T>> QueryAsync(string sql)
        {
            return _repo.QueryAsync(sql);
        }
        public int Execute(string sql)
        {
            return _repo.Execute(sql);
        }
        public int Execute(string sql, object param)
        {
            return _repo.Execute(sql, param);
        }
        public Task<int> ExecuteAsync(string sql)
        {
            return _repo.ExecuteAsync(sql);
        }
        public Task<int> ExecuteAsync(string sql, object param)
        {
            return _repo.ExecuteAsync(sql, param);
        }
        public bool Update(T obj)
        {
            return _repo.Update(obj);
        }
        public Task<bool> UpdateAsync(T obj)
        {
            return _repo.UpdateAsync(obj);
        }
        public bool Update(List<T> list)
        {
            return _repo.Update(list);
        }
        public Task<bool> UpdateAsync(List<T> list)
        {
            return _repo.UpdateAsync(list);
        }
        public bool Delete(T obj)
        {
            return _repo.Delete(obj);
        }
        public Task<bool> DeleteAsync(T obj)
        {
            return _repo.DeleteAsync(obj);
        }
        public T QueryFirst(string sql, object param)
        {
            return _repo.QueryFirst(sql, param);
        }
        public T QueryFirstOrDefault(string sql, object param)
        {
            return _repo.QueryFirstOrDefault(sql, param);
        }
        public async Task<T> QueryFirstOrDefaultAsync(string sql, object param)
        {
            return await _repo.QueryFirstOrDefaultAsync(sql, param);
        }
        public Task<IEnumerable<T>> ProcedureListAsync(string procedureName)
        {
            return _repo.ProcedureListAsync(procedureName);
        }
        public IEnumerable<T> Query(string sql, object param)
        {
            return _repo.Query(sql, param);
        }
        public Task<IEnumerable<T>> QueryAsync(string sql, object param)
        {
            return _repo.QueryAsync(sql, param);
        }
    }
}
