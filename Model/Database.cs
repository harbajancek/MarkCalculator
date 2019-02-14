using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.Reflection;

namespace Model
{
    public class Database
    {
        public string dataPath { get; }
        SQLiteAsyncConnection db;

        public Database()
        {
            dataPath = Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "MarkDatabase.db");
            defConstructor();
        }
        public Database(string databaseFilePath)
        {
            dataPath = databaseFilePath;
           defConstructor();
        }
        void defConstructor()
        {
             db = getDBConnection();
             db.CreateTableAsync<Mark>().Wait();
             db.CreateTableAsync<Subject>().Wait();
             db.CreateTableAsync<SubjectBook>().Wait();
        }

        public async Task<int> AddItem<T>(T item) where T : ATable
        {
            return await db.InsertAsync(item);
        }

        public async Task<List<T>> GetItem<T>(int index) where T : ATable, new()
        {
            return await db.Table<T>().Where(item => item.ID == index).ToListAsync();
        }

        public async Task<List<Subject>> GetSubjects(SubjectBook book)
        {
            return await db.Table<Subject>().Where(item => item.SubjectBookID == book.ID).ToListAsync();
        }

        public async Task<List<Mark>> GetMarks(Subject subject)
        {
            return await db.Table<Mark>().Where(item => item.SubjectID == subject.ID).ToListAsync();
        }

        public async Task<List<SubjectBook>> GetSubjectBooks()
        {
            var x = await db.Table<SubjectBook>().ToListAsync();
            return x;
        }

        public async Task<int> UpdateItem<T>(T item) where T : ATable
        {
            return await db.UpdateAsync(item);
        }

        public async Task<int> DeleteItem<T>(T item) where T : ATable
        {
            return await db.DeleteAsync<T>(item.ID);
        }

        SQLiteAsyncConnection getDBConnection()
        {
            return new SQLiteAsyncConnection(dataPath);
        }
    }
}
