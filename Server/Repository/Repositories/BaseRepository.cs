using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class BaseRepository<T> where T : class
    {
        public IDbConnection _conn;


        public T Get(int id)
        {
            return _conn.Get<T>(id);
        }

        public IEnumerable<T> GetList()
        {
            return _conn.GetAll<T>();
        }

        public long Insert(T entity)
        {

            return _conn.Insert<T>(entity);
        }

        public bool Update(T entity)
        {
            return _conn.Update<T>(entity);
        }

        public bool Delete(T entity)
        {
            return _conn.Delete<T>(entity);
        }

        #region Async
        public async Task<T> GetAsync(int id)
        {
            return await _conn.GetAsync<T>(id);
        }

        public async Task<IEnumerable<T>> GetListAsync()
        {
            return await _conn.GetAllAsync<T>();
        }

        public async Task<int> InsertAsync(T entity)
        {
            return await _conn.InsertAsync<T>(entity);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            return await _conn.UpdateAsync<T>(entity);
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            return await _conn.DeleteAsync<T>(entity);
        }

        #endregion

    }
}
