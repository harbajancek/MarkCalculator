using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppView
{
    
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateBookPage : ContentPage
	{
        public string text;
        public CreateBookPage ()
		{
			InitializeComponent ();
		}

        private void Button_Pressed(object sender, EventArgs e)
        {
            text = BookName.Text;
            if (!string.IsNullOrEmpty(text))
            {
                Navigation.PopModalAsync();
            }
            else
            {
                ErrorLabel.IsVisible = true;
            }
        }
    }
}