using LoginTutorial.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LoginTutorial
{
    public partial class MainPage : ContentPage
    {
        LoginViewModel VM;
        public MainPage(string username)
        {
            VM = new LoginViewModel(this.Navigation);
            BindingContext = VM;
            InitializeComponent();
            Username.Text = username;    
        }

        
    }
}
