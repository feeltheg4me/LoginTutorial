using LoginTutorial.Models;
using LoginTutorial.Views;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace LoginTutorial.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public INavigation navigation { get; set; }
        public ICommand BackCommand { get; set; }


        public BaseViewModel()
        {
            BackCommand = new AsyncCommand(Back, allowsMultipleExecutions: false);

        }
        public bool IsBusy { get; set; }
        public bool IsForm { get; set; }

        internal event Func<string, Task> DoDisplayAlert;

        internal event Func<BaseViewModel, bool, Task> DoNavigate;
        public virtual void OnAppearing()
        {
        }

        public virtual void OnDisappearing()
        {
        }
        public Task DisplayAlertAsync(string message)
        {
            return DoDisplayAlert?.Invoke(message) ?? Task.CompletedTask;
        }
        public async Task Back()
        {
            try
            {
                var currentPage = navigation.NavigationStack[navigation.NavigationStack.Count - 1] as BasePage;
                if (IsForm)
                {
                    if (currentPage.IsPageChanged)
                    {
                        var action = await App.Current.MainPage.DisplayActionSheet(currentPage.GetType().Name, "Oui", "Non", "Voulez-vous quitter la formulaire ?");


                        if (action.Equals("Oui"))
                        {
                            await navigation.PopAsync();
                        }
                        else
                        {
                            await navigation.PopAsync();
                        }
                    }
                    else
                    {
                        await navigation.PopAsync();
                    }
                }
            }
            catch (Exception x)
            {
                IsBusy = false;
                await App.Current.MainPage.DisplayAlert("Exception", x.Message, "ok");
            }
            finally
            {
                IsBusy = false;
            }
        }
        public Task NavigateAsync(BaseViewModel vm, bool showModal = false)
        {
            return DoNavigate?.Invoke(vm, showModal) ?? Task.CompletedTask;
        }

        


        #region INotify
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
            {
                return;
            }
            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion
    }
}
