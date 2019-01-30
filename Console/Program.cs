using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model;

namespace ConsoleView
{
    class Program
    {
        static void Main(string[] args)
        {
            SubjectBook book = new SubjectBook()
            {
                Name = "jmeno",
                Year = 2018
            };

            int x = AddBook(book).Result;

            Console.Read();


            ListBooks();

            Console.Read();
        }

        private static async Task<int> AddBook(SubjectBook book)
        {
            return await MarkFunctions.AddItem(book);
        }

        private static async void ListBooks()
        {
            var books = await MarkFunctions.GetBooks();

            foreach (var book in books)
            {
                Console.WriteLine(book.Name + ":");
                Console.WriteLine("\tID: " + book.ID);
                Console.WriteLine("\tYear: " + book.Year);
            }
        }
    }
}
