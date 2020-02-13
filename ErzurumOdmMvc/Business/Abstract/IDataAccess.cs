using System.Collections.Generic;
using System.Threading.Tasks;

namespace ErzurumOdmMvc.Business.Abstract
{
    public interface IDataAccess<T>
    {
        T Procedure(string procedureName);
        T Procedure(string procedureName, object param);
        Task<T> ProcedureAsync(string procedureName, object param);
        void ProcedureGES(string procedureName, object param);
        void ProcedureGESAsync(string procedureName, object param);

        IEnumerable<T> ProcedureList(string procedureName);
        Task<IEnumerable<T>> ProcedureListAsync(string procedureName);
        IEnumerable<T> ProcedureList(string procedureName, object param);
        Task<IEnumerable<T>> ProcedureListAsync(string procedureName, object param);
        IEnumerable<T> List();
        Task<IEnumerable<T>> ListAsync();
        long Insert(T obj);
        Task<int> InsertAsync(T obj);
        T Find(int id);
        Task<T> FindAsync(int id);
        T Find(string sql);
        T Find(string sql, object param);
        Task<T> FindAsync(string sql);
        Task<T> FindAsync(string sql, object param);
        T QueryFirst(string sql, object param);
        IEnumerable<T> Query(string sql);
        IEnumerable<T> Query(string sql, object param);
        Task<IEnumerable<T>> QueryAsync(string sql);
        Task<IEnumerable<T>> QueryAsync(string sql, object param);
        T QueryFirstOrDefault(string sql, object param);

        object ProcedureScalar(string procedureName, object param);
        object Scalar(string sql);
        object Scalar(string sql, object param);
        Task<object> ScalarAsync(string sql);
        Task<object> ScalarAsync(string sql, object param);
        int Execute(string sql);
        int Execute(string sql, object param);
        Task<int> ExecuteAsync(string sql);
        Task<int> ExecuteAsync(string sql, object param);
        bool Update(T obj);
        Task<bool> UpdateAsync(T obj);
        bool Update(List<T> list);
        Task<bool> UpdateAsync(List<T> list);
        bool Delete(T obj);
        Task<bool> DeleteAsync(T obj);
    }
}
