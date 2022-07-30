using LoginTutorial.DAO;
using LoginTutorial.DAO.Interfaces;
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
    public class UserViewModel : BaseViewModel
    {
        ISignUpService signUpService = DependencyService.Get<ISignUpService>();
        IUserDao userDao = DependencyService.Get<IUserDao>();
        ILoginService loginService = DependencyService.Get<ILoginService>();
        InitDB initDB = DependencyService.Get<InitDB>();
        //
        public User CurrentUser { get; set; }
        public string LoginError { get; set; }
        //
        public ICommand SignUpCommand { get; set; }
        public ICommand GoLoginCommand { get; set; }
        public ICommand ForgotPasswordCommand { get; set; }
        public ICommand GoSignUpCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        //

        public UserViewModel()
        {
            ForgotPasswordCommand = new Command(OnForgotPassword);
            GoSignUpCommand = new Command(OnGoSignUp);
            LoginCommand = new Command(async () => await OnLoginAsync());
            LogoutCommand = new Command(OnLogout);
            SignUpCommand = new Command(async () => await OnSignUpAsync());
            GoLoginCommand = new Command(OnGoLogin);
            CurrentUser = new User();
            UserName = Settings.LastUsedUserName;
        }


        #region UserViewModel
        private User user;
        public User User
        {
            get { return user; }
            set { user = value; OnPropertyChanged(); }
        }
        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value; OnPropertyChanged();
                Settings.LastUsedUserName = value;
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(); }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; OnPropertyChanged(); }
        }
        private string email;


        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(); }
        }
        private string phone;
        public string Phone
        {
            get { return phone; }
            set { phone = value; OnPropertyChanged(); }
        }

        private bool turnErrorMessage = false;
        public bool TurnErrorMessage
        {
            get { return turnErrorMessage; }
            set { turnErrorMessage = value; OnPropertyChanged(); }
        }
        #endregion

        #region SignUpViewModel
        // Sign Up
        public async Task OnSignUpAsync()
        {
            var navigationStack = navigation.NavigationStack;
            var currentPage = navigationStack[navigationStack.Count - 1];
            bool isvalid = ValidationHelper.IsFormValid(CurrentUser, currentPage);
            if (isvalid)
            {
                try
                {
                    var isSignedUp = await signUpService.IsSignedUpAsync(CurrentUser.UserName, CurrentUser.Password, CurrentUser.Email, CurrentUser.Phone);
                    if (!isSignedUp)
                    {

                        await userDao.AddUserAsync(CurrentUser);
                        await App.Current.MainPage.Navigation.PopAsync();
                        ErrorMessage = "";

                    }
                    else
                    {
                        ErrorMessage = "User Name or Email already exists!";
                        TurnErrorMessage = true;
                    }
                }
                catch (Exception ex)
                {

                    ErrorMessage = ex.Message;
                }
            }
        }

        // Go to LoginPage
        public void OnGoLogin(object obj)
        {
            App.Current.MainPage.Navigation.PopAsync();

        }

        #endregion

        #region LoginViewModel
        // Login
        public async Task OnLoginAsync()
        {

            try
            {

                var currentPage = App.Current.MainPage.Navigation.NavigationStack[0];
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
            catch (Exception ex)
            {

                await App.Current.MainPage.DisplayAlert("hi", ex.Message, "ok");
            }
        }

        // Go to SignUpPage
        public void OnGoSignUp(object obj)
        {
            navigation.PushAsync(new SignUpPage());
            ErrorMessage = "";

        }

        // Go to ForgotPasswordPage
        public void OnForgotPassword(object obj)
        {
            navigation.PushAsync(new ForgotPasswordPage());
            ErrorMessage = "";
        }
        // Logout
        public void OnLogout()
        {
            App.Current.MainPage.Navigation.PopAsync();
        }
        #endregion

    }
}
