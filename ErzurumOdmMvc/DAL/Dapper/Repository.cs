using Dapper;
using Dapper.Contrib.Extensions;
using MySql.Data.MySqlClient;
using ErzurumOdmMvcDAL.Abstract;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace ErzurumOdmMvcDAL.Dapper
{
    public class Repository<T> : IDapperRepository<T> where T : class
    {
        public MySqlConnection GetOpenConnection()
        {
            MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["DatabaseContext"].ConnectionString);
            connection.Open();
            return connection;
        }
        public T Procedure(string procedureName)
        {
            return GetOpenConnection().QueryFirstOrDefault<T>(procedureName, commandType: CommandType.StoredProcedure);
        }
        public T Procedure(string procedureName, object param)
        {
            return GetOpenConnection().QueryFirstOrDefault<T>(procedureName, param, commandType: CommandType.StoredProcedure);
        }
        
        public async Task<T> ProcedureAsync(string procedureName, object param)
        {
            return await GetOpenConnection().QueryFirstOrDefaultAsync<T>(procedureName, param, commandType: CommandType.StoredProcedure);
        }
        public void ProcedureGES(string procedureName, object param)
        {
            GetOpenConnection().Execute(procedureName, param, commandType: CommandType.StoredProcedure);
        }
        public async void ProcedureGESAsync(string procedureName, object param)
        {
            await GetOpenConnection().ExecuteAsync(procedureName, param, commandType: CommandType.StoredProcedure);
        }
        public IEnumerable<T> ProcedureList(string procedureName)
        {
            var list = GetOpenConnection().Query<T>(procedureName, commandType: CommandType.StoredProcedure);
            return list;
        }
        public async Task<IEnumerable<T>> ProcedureListAsync(string procedureName)
        {
            var list = await GetOpenConnection().QueryAsync<T>(procedureName, commandType: CommandType.StoredProcedure);
            return list;
        }
        public IEnumerable<T> ProcedureList(string procedureName, object param)
        {
            IEnumerable<T> list = GetOpenConnection().Query<T>(procedureName, param, commandType: CommandType.StoredProcedure);
            return list;
        }
        public async Task<IEnumerable<T>> ProcedureListAsync(string procedureName, object param)
        {
            IEnumerable<T> list = await GetOpenConnection().QueryAsync<T>(procedureName, param, commandType: CommandType.StoredProcedure);
            return list;
        }
        public IEnumerable<T> List()
        {
            using (var conn = GetOpenConnection())
            {
               IEnumerable<T> obj = conn.GetAll<T>();
                return obj;
            }
        }
        public async Task<IEnumerable<T>> ListAsync()
        {
            using (var conn = GetOpenConnection())
            {
                IEnumerable<T> obj = await conn.GetAllAsync<T>();
                return obj;
            }
        }
        public long Insert(T obj)
        {
            long sonuc;
            using (var conn = GetOpenConnection())
            {
                sonuc = conn.Insert(obj);
            }
            return sonuc;
        }
        public async Task<int> InsertAsync(T obj)
        {
            int sonuc;
            using (var conn = GetOpenConnection())
            {
                sonuc = await conn.InsertAsync(obj);
            }
            return sonuc;
        }
        public T Find(int id)
        {
            using (var conn = GetOpenConnection())
            {
                T obj = conn.Get<T>(id);
                return obj;
            }
        }
        public async Task<T> FindAsync(int id)
        {
            using (var conn = GetOpenConnection())
            {
                T obj = await conn.GetAsync<T>(id);
                return obj;
            }
        }
        public T Find(string sql)
        {
            using (var conn = GetOpenConnection())
            {
                T obj = conn.QueryFirstOrDefault<T>(sql);
                return obj;
            }
        }
        public T Find(string sql, object param)
        {
            using (var conn = GetOpenConnection())
            {
                T obj = conn.QueryFirstOrDefault<T>(sql, param);
                return obj;
            }
        }
        public async Task<T> FindAsync(string sql)
        {
            using (var conn = GetOpenConnection())
            {
                T obj = await conn.QueryFirstOrDefaultAsync<T>(sql);
                return obj;
            }
        }
        public async Task<T> FindAsync(string sql, object param)
        {
            using (var conn = GetOpenConnection())
            {
                T obj = await conn.QueryFirstOrDefaultAsync<T>(sql, param);
                return obj;
            }
        }
        public T QueryFirst(string sql, object param)
        {
            using (var conn = GetOpenConnection())
            {
                T obj = conn.QueryFirst<T>(sql, param);
                return obj;
            }
        }
        //Select gibi Sorgu işlemleri için
        public IEnumerable<T> Query(string sql)
        {
            using (var conn = GetOpenConnection())
            {
                IEnumerable<T> obj = conn.Query<T>(sql);
                return obj;
            }
        }
        public IEnumerable<T> Query(string sql, object param)
        {
            using (var conn = GetOpenConnection())
            {
                IEnumerable<T> obj = conn.Query<T>(sql, param);
                return obj;
            }
        }
        //Select gibi Sorgu işlemleri için
        public async Task<IEnumerable<T>> QueryAsync(string sql)
        {
            using (var conn = GetOpenConnection())
            {
                IEnumerable<T> obj = await conn.QueryAsync<T>(sql);
                return obj;
            }
        }
        public async Task<IEnumerable<T>> QueryAsync(string sql, object param)
        {
            using (var conn = GetOpenConnection())
            {
                IEnumerable<T> obj = await conn.QueryAsync<T>(sql, param);
                return obj;
            }
        }
        public T QueryFirstOrDefault(string sql, object param)
        {
            using (var conn = GetOpenConnection())
            {
                T obj = conn.QueryFirstOrDefault<T>(sql, param);
                return obj;
            }
        }
        public async Task<T> QueryFirstOrDefaultAsync(string sql, object param)
        {
            using (var conn = GetOpenConnection())
            {
                T obj = await conn.QueryFirstOrDefaultAsync<T>(sql, param);
                return obj;
            }
        }
        public object ProcedureScalar(string procedureName, object param)
        {
            return GetOpenConnection().ExecuteScalar(procedureName, param, commandType: CommandType.StoredProcedure);
        }
        public object Scalar(string sql)
        {
            object sonuc;
            using (var conn = GetOpenConnection())
            {
                sonuc = conn.ExecuteScalar(sql);
            }
            return sonuc;
        }
        public object Scalar(string sql, object param)
        {
            object sonuc;
            using (var conn = GetOpenConnection())
            {
                sonuc = conn.ExecuteScalar(sql,param);
            }
            return sonuc;
        }
        public Task<object> ScalarAsync(string sql)
        {
            Task<object> sonuc;
            using (var conn = GetOpenConnection())
            {
                sonuc = conn.ExecuteScalarAsync(sql);
            }
            return sonuc;
        }
        public Task<object> ScalarAsync(string sql, object param)
        {
            Task<object> sonuc;
            using (var conn = GetOpenConnection())
            {
                sonuc = conn.ExecuteScalarAsync(sql, param);
            }
            return sonuc;
        }
        public int Execute(string sql)
        {
            int sonuc;
            using (var conn = GetOpenConnection())
            {
                sonuc = conn.Execute(sql);
            }
            return sonuc;
        }
        public async Task<int> ExecuteAsync(string sql)
        {
            int sonuc;
            using (var conn = GetOpenConnection())
            {
                sonuc = await conn.ExecuteAsync(sql);
            }
            return sonuc;
        }
        public int Execute(string sql, object param)
        {
            int sonuc;
            using (var conn = GetOpenConnection())
            {
                sonuc = conn.Execute(sql, param);
            }
            return sonuc;
        }
        public async Task<int> ExecuteAsync(string sql, object param)
        {
            int sonuc;
            using (var conn = GetOpenConnection())
            {
                sonuc = await conn.ExecuteAsync(sql, param);
            }
            return sonuc;
        }
        public bool Update(T obj)
        {
            bool sonuc;
            using (var conn = GetOpenConnection())
            {
                sonuc = conn.Update(obj);
            }
            return sonuc;
        }
        public async Task<bool> UpdateAsync(T obj)
        {
            bool sonuc;
            using (var conn = GetOpenConnection())
            {
                sonuc = await conn.UpdateAsync(obj);
            }
            return sonuc;
        }
        public bool Update(List<T> list)
        {
            bool sonuc;
            using (var conn = GetOpenConnection())
            {
                sonuc = conn.Update(list);
            }
            return sonuc;
        }
        public async Task<bool> UpdateAsync(List<T> list)
        {
            bool sonuc;
            using (var conn = GetOpenConnection())
            {
                sonuc = await conn.UpdateAsync(list);
            }
            return sonuc;
        }
        public bool Delete(T obj)
        {
            bool sonuc;
            using (var conn = GetOpenConnection())
            {
                sonuc = conn.Delete(obj);
            }
            return sonuc;
        }
        public async Task<bool> DeleteAsync(T obj)
        {
            bool sonuc;
            using (var conn = GetOpenConnection())
            {
                sonuc = await conn.DeleteAsync(obj);
            }
            return sonuc;
        }
    }
}
