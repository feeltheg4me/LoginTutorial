using LoginTutorial.Helpers;
using LoginTutorial.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LoginTutorial.Views
{
    public class BasePage : ContentPage
    {

        public static readonly BindableProperty IsFormProperty =
       BindableProperty.Create(nameof(IsForm), typeof(bool), typeof(BasePage), false, BindingMode.TwoWay);

        public bool IsForm
        {
            get { return (bool)GetValue(IsFormProperty); }
            set { SetValue(IsFormProperty, value); }
        }
        public BaseViewModel VM;
        public bool IsPageChanged { get; set; }
        public BasePage()
        {
            NavigationPage.SetHasBackButton(this, false);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            IsPageChanged = false;
            var Checkboxs = this.GetChildren<CheckBox>().ToList();
            var Entrys = this.GetChildren<Entry>().ToList();
            var DatePickers = this.GetChildren<DatePicker>().ToList();
            var Pickers = this.GetChildren<Picker>().ToList();

            if (Checkboxs != null)
            {
                if (Checkboxs.Count > 0)
                {
                    Checkboxs.ForEach(c => c.CheckedChanged -= OnCheckBokChecked);
                    Checkboxs.ForEach(c => c.CheckedChanged += OnCheckBokChecked);
                }
            }
            if (Entrys != null)
            {
                if (Entrys.Count > 0)
                {
                    Entrys.ForEach(e => e.TextChanged -= OnEntyTextChanged);
                    Entrys.ForEach(e => e.TextChanged += OnEntyTextChanged);
                }
            }
            if (DatePickers != null)
            {
                if (DatePickers.Count > 0)
                {
                    DatePickers.ForEach(e => e.DateSelected -= OnDatePickerDateSelected);
                    DatePickers.ForEach(e => e.DateSelected += OnDatePickerDateSelected);

                }
            }
            if (Pickers != null)
            {
                if (Pickers.Count > 0)
                {
                    Pickers.ForEach(e => e.SelectedIndexChanged -= OnpickerItemSelected);
                    Pickers.ForEach(e => e.SelectedIndexChanged += OnpickerItemSelected);
                }
            }
            SetupBinding(BindingContext);
        }



        protected override void OnDisappearing()
        {
            TearDownBinding(BindingContext);

            base.OnDisappearing();
        }

        protected void SetupBinding(object bindingContext)
        {
            if (bindingContext is BaseViewModel vm)
            {
                VM = vm;
                vm.navigation = Navigation;
                vm.DoDisplayAlert += OnDisplayAlert;
                vm.DoNavigate += OnNavigate;
                vm.OnAppearing();
            }
        }
        protected override bool OnBackButtonPressed()
        {
            VM.BackCommand.Execute(null);
            return true;
        }
        protected void TearDownBinding(object bindingContext)
        {
            if (bindingContext is BaseViewModel vm)
            {
                vm.navigation = Navigation;
                vm.OnDisappearing();
                vm.DoDisplayAlert -= OnDisplayAlert;
                vm.DoNavigate -= OnNavigate;
            }
        }
        void OnCheckBokChecked(object sender, CheckedChangedEventArgs e)
        {
            IsPageChanged = true;
        }
        void OnEntyTextChanged(object sender, TextChangedEventArgs e)
        {
            IsPageChanged = true;
        }
        void OnDatePickerDateSelected(object sender, DateChangedEventArgs e)
        {
            IsPageChanged = true;
        }
        void OnpickerItemSelected(object sender, EventArgs e)
        {
            IsPageChanged = true;
        }
        Task OnDisplayAlert(string message)
        {
            return DisplayAlert(Title, message, "OK");
        }

        Task OnNavigate(BaseViewModel vm, bool showModal)
        {
            var name = vm.GetType().Name;
            name = name.Replace("ViewModel", "Page");

            var ns = GetType().Namespace;
            var pageType = Type.GetType($"{ns}.{name}");

            var page = (BasePage)Activator.CreateInstance(pageType);
            page.BindingContext = vm;

            return showModal ? Navigation.PushModalAsync(page) : Navigation.PushAsync(page);
        }
    }
}