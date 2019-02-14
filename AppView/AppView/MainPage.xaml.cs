using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Model;
using System.Diagnostics;
using System.Threading;

namespace AppView
{
    public partial class MainPage : ContentPage
    {
        Database db = new Database(DependencyService.Get<IFileHelper>().GetLocalFilePath("MarkDatabase.db"));
        public MainPage()
        {
            InitializeComponent();

            displayBooks();
        }

        private void displayBooks()
        {
            StackOfBooks.Children.Clear();

            Task<List<SubjectBook>> booksTask = db.GetSubjectBooks();
            List<SubjectBook> books = new List<SubjectBook>();

            booksTask.ContinueWith(t =>
            {
                books = booksTask.Result;

                if (books.Count == 0)
                {
                    ErrorLabel.IsVisible = true;
                }
                else
                {
                    ErrorLabel.IsVisible = false;

                    books.ForEach(book =>
                    {
                        StackLayout bookStack = new StackLayout()
                        {
                            Orientation = StackOrientation.Horizontal
                        };

                        Button bookOpen = new Button
                        {
                            Text = book.Name + " " + book.Year
                        };

                        Button bookDelete = new Button
                        {
                            Text = "Delete"
                        };
                        bookDelete.Clicked += bookDeleteClickAsync;
                        bookDelete.BindingContext = book;


                        bookStack.Children.Add(bookOpen);
                        bookStack.Children.Add(bookDelete);
                        
                        StackOfBooks.Children.Add(bookStack);
                    });
                }

            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void bookDeleteClickAsync(object sender, EventArgs e)
        {
            db.DeleteItem(((Button)sender).BindingContext as SubjectBook).ContinueWith(t => 
            {
                t.Wait();

                displayBooks();
            });
        }

        private void createBookPressed(object sender, EventArgs e)
        {
            Debug.WriteLine(DependencyService.Get<IFileHelper>().GetLocalFilePath("MarkDatabase.db"));
            CreateBookPage page = new CreateBookPage();
            page.Disappearing += async (sender2, e2) =>
            {
                await db.AddItem<SubjectBook>(new SubjectBook
                {
                    Name = page.text,
                    Year = DateTime.Now.Year
                });

                displayBooks();
            };
            Navigation.PushModalAsync(page);
        }
    }
}
