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
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            await ListBooks();

            Console.Read();
        }

        private static async Task<int> AddBook(SubjectBook book)
        {
            return await MarkFunctions.AddItem(book);
        }

        private static async Task ListBooks()
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
