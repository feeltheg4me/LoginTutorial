using LoginTutorial.DAO.Interfaces;
using LoginTutorial.Helpers;
using LoginTutorial.Models;
using LoginTutorial.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LoginTutorial.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {

        ISignUpService signUpService = DependencyService.Get<ISignUpService>();
        IUserDao userDao = DependencyService.Get<IUserDao>();
        public ICommand SignUpCommand { get; set; }
        public ICommand GoLoginCommand { get; set; }
        public User CurrentUser { get; set; }
        public SignUpViewModel(INavigation _navigation)
        {
            navigation = _navigation;
            SignUpCommand = new Command(async() => await OnSignUpAsync());
            GoLoginCommand = new Command(OnGoLogin);
            CurrentUser = new User();
        }

        // Sign Up
        private async Task OnSignUpAsync()
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

                ErrorMessage=ex.Message;
            }
            }
        }


      
        // Go to LoginPage
        private void OnGoLogin(object obj)
        {
            App.Current.MainPage.Navigation.PopAsync();
            
        }




    }
}
