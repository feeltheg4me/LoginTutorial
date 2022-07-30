using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoginTutorial.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : BasePage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        // Show password CheckBox
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
            if (ShowPasswordCheckBox.IsChecked)
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
            User_PasswordError.Text = "";
            User_UserNameError.Text = "";
            base.OnAppearing();
        }


    }
}