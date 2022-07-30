using LoginTutorial.Helpers;
using LoginTutorial.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoginTutorial.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : BasePage
    {

        public SignUpPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            IsForm = true;
            User_PasswordError.Text = "";
            User_UserNameError.Text = "";
            User_EmailError.Text = "";
            User_PhoneError.Text = "";
            base.OnAppearing();
        }

    }
}