using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace teshib
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Person>().Wait();
        }

        public Task<List<Person>> GetPeopleAsync()
        {
            return _database.Table<Person>().ToListAsync();
        }

        public Task<int> SavePersonAsync(Person person)
        {
            return _database.InsertAsync(person);
        }
        public Task<int> DeletePersonAsync(Person person)
        {
            return _database.DeleteAsync(person);
        }
        public Task<List<Person>> DeleteTable()
        {
            // SQL queries are also possible
            return _database.QueryAsync<Person>("DELETE FROM Person");
        }
        public Task<List<Person>> InsertInto(string name,object value )
        {
            // SQL queries are also possible
            return _database.QueryAsync<Person>("INSERT INTO Person(" + name +") VALUES(" + Convert.ToString(value) + ")");
        }
    }
}
