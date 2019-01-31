using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model;

namespace ConsoleView
{
    class Program
    {
        static async Task Main(string[] args)
        {
            DatabaseViewer dbv = new DatabaseViewer();
            await dbv.uAddBook();

            await dbv.ListBooks();

            Console.Read();
        }
    }
}
