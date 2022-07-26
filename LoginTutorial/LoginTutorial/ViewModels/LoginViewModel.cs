using LoginTutorial.DAO;
using LoginTutorial.Helpers;
using LoginTutorial.Models;
using LoginTutorial.Services.Interfaces;
using LoginTutorial.Views;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LoginTutorial.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        ILoginService loginService = DependencyService.Get<ILoginService>();
        InitDB initDB = DependencyService.Get<InitDB>();
        public ICommand ForgotPasswordCommand { get; set; }
        public ICommand GoSignUpCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public User CurrentUser { get; set; }
        public LoginViewModel(INavigation _navigation)
        {
            navigation = _navigation;
            ForgotPasswordCommand = new Command(OnForgotPassword);
            GoSignUpCommand = new Command(OnGoSignUp);
            LoginCommand = new Command(async() => await OnLoginAsync());
            LogoutCommand = new Command(OnLogout);
            CurrentUser = new User();
            
        }


        // Logout
        private void OnLogout()
        {
            App.Current.MainPage.Navigation.PopAsync();
        }

        public string LoginError { get; set; }
        // Login
        public async Task OnLoginAsync()
        {
            var currentPage = App.Current.MainPage;
            bool isvalid = ValidationHelper.IsFormValid(CurrentUser, currentPage);
            if (isvalid)
            {
                CurrentUser.UserName = CurrentUser.UserName.Trim();
            try
            {
                
                var isLogedin = await loginService.IsLogedinAsync(CurrentUser.UserName, CurrentUser.Password);
                if (isLogedin)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new MainPage(CurrentUser.UserName));
                }
                    
                else
                {
                    ErrorMessage = "Enter a valid User Name and Password!";
                    TurnErrorMessage = true;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            }
        }

       

        // Go to SignUpPage
        public void OnGoSignUp(object obj)
        {
            App.Current.MainPage.Navigation.PushAsync(new SignUpPage());
            ErrorMessage = "";

        }

        // Go to ForgotPasswordPage
        private void OnForgotPassword(object obj)
        {
            App.Current.MainPage.Navigation.PushAsync(new ForgotPasswordPage());
            ErrorMessage = "";
        }

       
    }
}
