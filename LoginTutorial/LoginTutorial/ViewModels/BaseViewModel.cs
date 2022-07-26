using LoginTutorial.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace LoginTutorial.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public INavigation navigation;

        public event PropertyChangedEventHandler PropertyChanged;

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
            set { userName = value; OnPropertyChanged(); }
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



        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
            {
                return;
            }
            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
