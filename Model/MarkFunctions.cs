using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MarkFunctions
    {
        public static async Task<float> GetAverage(Subject subject)
        {
            List<Mark> marks = await GetMarks(subject);

            float allMarkValue = 0;
            float markCount = 0;

            foreach (var item in marks)
            {
                allMarkValue += item.Value * item.Weight;
                markCount += item.Weight;
            }

            return allMarkValue / markCount;
        }

        public static async Task<List<Mark>> GetMarks(Subject subject)
        {
            Database db = new Database();
            return await db.GetMarks(subject);
        }

        public static async Task<List<Subject>> GetSubjects(SubjectBook book)
        {
            Database db = new Database();
            return await db.GetSubjects(book);
        }

        public static async Task<List<SubjectBook>> GetBooks()
        {
            Database db = new Database();
            return await db.GetSubjectBooks();
        }

        public static async Task<int> AddItem<T>(T item) where T : ATable
        {
            Database db = new Database();
            return await db.AddItem(item);
        }
    }
}
