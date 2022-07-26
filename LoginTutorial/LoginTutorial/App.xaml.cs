using LoginTutorial.DAO;
using LoginTutorial.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoginTutorial
{
    public partial class App : Application
    {
        InitDB initDB = DependencyService.Get<InitDB>();
        public App()
        {
            InitializeComponent();
            initDB.initDataBase();
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
