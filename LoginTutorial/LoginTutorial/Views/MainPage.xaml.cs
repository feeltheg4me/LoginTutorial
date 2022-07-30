using LoginTutorial.ViewModels;
using LoginTutorial.Views;
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
    public partial class MainPage : BasePage
    {

        public MainPage(string username)
        {

            InitializeComponent();
            Username.Text = username;    
        }

        
    }
}
