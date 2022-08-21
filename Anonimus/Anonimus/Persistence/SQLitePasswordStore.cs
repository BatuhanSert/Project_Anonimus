using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Anonimus.Model;
using Anonimus.ViewModel;
using SQLite;

namespace Anonimus.Persistence
{
    public class SQLitePasswordStore : IPasswordStore
    {
        private SQLiteAsyncConnection _connection;

        public SQLitePasswordStore(ISQLiteDb db)
        {
            _connection = db.GetConnection();
            _connection.CreateTableAsync<Password>();
        }

        public async Task<IEnumerable<Password>> GetPasswordsAsync()
        {
            return await _connection.Table<Password>().ToListAsync();
        }

        public async Task DeletePassword(Password password)
        {
            await _connection.DeleteAsync(password);
        }

        public async Task AddPassword(Password password)
        {
            await _connection.InsertAsync(password);
        }

        public async Task UpdatePassword(Password password)
        {
            await _connection.UpdateAsync(password);
        }

        public async Task<Password> GetPassword(int id)
        {
            return await _connection.FindAsync<Password>(id);
        }
    }
}
