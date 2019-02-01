using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleView
{
    class DatabaseViewer
    {
        int bookID;
        int subjectID;

        public DatabaseViewer()
        {
            bookID = 0;
            subjectID = 0;
        }

        internal async Task<int> AddBook(SubjectBook book)
        {
            return await MarkFunctions.AddItem(book);
        }
        internal async Task ListBooks()
        {
            var books = await MarkFunctions.GetBooks();

            string consoleWrite = string.Empty;

            foreach (var book in books)
            {
                consoleWrite += "ID: " + book.ID;
                consoleWrite += " Name: " + book.Name;
                consoleWrite += " Year: " + book.Year;
                Console.WriteLine();
            }

            Console.WriteLine(consoleWrite);
        }
        async Task selectBookID()
        {
            await ListBooks();

            string stringID;
            int ID;

            do
            {
                Console.Write("Select ID: ");
                stringID = Console.ReadLine();
            }
            while (int.TryParse(stringID, out ID));

            bookID = ID;
        }

        async Task selectSubjectID()
        {
            await ListBooks();

            string stringID;
            int ID;

            do
            {
                Console.Write("Select ID: ");
                stringID = Console.ReadLine();
            }
            while (int.TryParse(stringID, out ID));

            subjectID = ID;
        }

        void deselectSubjectID()
        {
            subjectID = 0;
        }

        void deselectBookID()
        {
            bookID = 0;
        }

        internal async Task uAddBook()
        {
            await MarkFunctions.AddItem(uCreateBook());
        }
        internal SubjectBook uCreateBook()
        {
            Console.Write("Name: ");
            string name = Console.ReadLine();

            string stringYear;
            int year;

            do
            {
                Console.Write("Year: ");
                stringYear = Console.ReadLine();
            }
            while (!int.TryParse(stringYear, out year));

            return new SubjectBook()
            {
                Name = name,
                Year = year
            };
        }
        internal async Task uAddSubject()
        {
            await ListBooks();

            string stringID;
            int ID;

            do
            {
                Console.Write("Select book ID: ");
                stringID = Console.ReadLine();
            }
            while (int.TryParse(stringID, out ID));

            await MarkFunctions.AddItem(uCreateSubject(ID));
        }

        internal async Task uAddMark()
        {
            await ListBooks();

            string stringID;
            int ID;

            do
            {
                Console.Write("Select book ID: ");
                stringID = Console.ReadLine();
            }
            while (int.TryParse(stringID, out ID));

            await MarkFunctions.AddItem(await uCreateSubject(ID));
        }
        internal async Task<Subject> uCreateSubject(int bookID)
        {
            if (bookID == 0)
            {
                await selectBookID();
            }

            Console.Write("Name: ");
            string name = Console.ReadLine();

            return new Subject()
            {
                Name = name,
                SubjectBookID = bookID
            };
        }
    }
}
