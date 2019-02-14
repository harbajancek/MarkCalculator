using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Model;

namespace AppView
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BookList : ContentPage
	{
        Database db = new Database(DependencyService.Get<IFileHelper>().GetLocalFilePath("MarkDatabase.db"));
        SubjectBook book;
        public BookList(SubjectBook book)
		{
			InitializeComponent();

            this.book = book;

            displayMarks();
        }

        private void displayMarks()
        {
            Task<List<Subject>> task = db.GetSubjects(book);
            List<Subject> subjects = new List<Subject>();

            task.ContinueWith(t => { subjects = t.Result; }).Wait();

            if (subjects.Count == 0)
            {
                ErrorNoSubjects.IsVisible = true;
            }
            else
            {
                ErrorNoSubjects.IsVisible = false;

                foreach (var subject in subjects)
                {
                    List<Mark> marks = new List<Mark>();
                    db.GetMarks(subject).ContinueWith(t => { marks = t.Result; }).Wait();

                    StackLayout ssl = new StackLayout() { Orientation = StackOrientation.Horizontal };

                    ssl.Children.Add(new Label() { Text = subject.Name });

                    ssl.Children.Add(new Label() { Text = MarkFunctions.GetAverage(marks).ToString("R") });

                    foreach (var mark in marks)
                    {
                        ssl.Children.Add(new Label() { Text = mark.Value + "(" + mark.Weight + ")" });
                    }
                }
            }
        }
	}
}