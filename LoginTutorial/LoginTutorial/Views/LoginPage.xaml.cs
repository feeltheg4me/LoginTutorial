using LoginTutorial.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoginTutorial.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginViewModel VM;
        public LoginPage()
        {
            VM = new LoginViewModel(this.Navigation);
            BindingContext = VM;
            InitializeComponent();
        }

        
        private void ShowPasswordCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {

            if (PasswordEntry.IsPassword)
            {
                PasswordEntry.IsPassword = false;
            }

            else
                PasswordEntry.IsPassword = true;

        }


        // Show Password Label Tapped
        private void ShowPassword_Tapped(object sender, EventArgs e)
        {
            if(ShowPasswordCheckBox.IsChecked)
            {
                ShowPasswordCheckBox.IsChecked = false;
            }
            else
            {
                ShowPasswordCheckBox.IsChecked = true;
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            PasswordEntry.IsPassword = true;
            UserNameEntry.Text = "";
            PasswordEntry.Text = "";        }

    }
}