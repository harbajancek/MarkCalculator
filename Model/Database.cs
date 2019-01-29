using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Model
{
    internal class Database
    {
        public string dataPath { get; }
        private SQLiteAsyncConnection db;

        public Database()
        {
            dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MarkDatabase.db");
        }
        public Database(string databaseFilePath)
        {
            dataPath = databaseFilePath;
        }
        async void defConstructor()
        {
            db = getDBConnection();
            await db.CreateTableAsync<Mark>();
            await db.CreateTableAsync<Subject>();
            await db.CreateTableAsync<SubjectBook>();
        }

        public async Task<int> AddItem<T>(T item) where T : ATable
        {
            return await db.InsertAsync(item);
        }

        public async Task<List<T>> GetItem<T>(int index) where T : ATable, new()
        {
            return await db.Table<T>().Where(item => item.ID == index).ToListAsync();
        }

        public async Task<int> UpdateItem<T>(T item) where T : ATable
        {
            return await db.UpdateAsync(item);
        }

        public async Task<int> DeleteItem<T>(int index) where T : ATable
        {
            return await db.DeleteAsync<T>(index);
        }

        SQLiteAsyncConnection getDBConnection()
        {
            return new SQLiteAsyncConnection(dataPath);
        }
    }
}
