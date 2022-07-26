using LoginTutorial.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoginTutorial.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        SignUpViewModel VM;
        public SignUpPage()
        {
            VM = new SignUpViewModel(this.Navigation);
            BindingContext = VM;
            InitializeComponent();
        }


    }
}