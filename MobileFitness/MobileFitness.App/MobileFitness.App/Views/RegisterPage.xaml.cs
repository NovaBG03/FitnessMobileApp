namespace MobileFitness.App.Views
{
    using System;
    using System.Threading.Tasks;

    using MobileFitness.App.ViewModels;
    using Newtonsoft.Json;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            var vm = new RegisterViewModel();
            this.BindingContext = vm;
            vm.DisplayInvalidPrompt += (string message) => DisplayAlert("Login", message, "Ok");

            this.BackgroundColor = Constants.BarBackgroundColor;

            this.UsernameLbl.TextColor = Constants.MainTextColor;
            this.EmailLbl.TextColor = Constants.MainTextColor;
            this.PasswordLbl.TextColor = Constants.MainTextColor;
            this.ConfirmPasswordLbl.TextColor = Constants.MainTextColor;
            this.BirthdateLbl.TextColor = Constants.MainTextColor;
            this.GoalLbl.TextColor = Constants.MainTextColor;
            this.GenderLbl.TextColor = Constants.MainTextColor;
            this.WeightLbl.TextColor = Constants.MainTextColor;
            this.HeightLbl.TextColor = Constants.MainTextColor;

            this.UsernameEntry.BackgroundColor = Constants.EntryBackgroundColor;
            this.EmailEntry.BackgroundColor = Constants.EntryBackgroundColor;
            this.PasswordEntry.BackgroundColor = Constants.EntryBackgroundColor;
            this.ConfirmPasswordEntry.BackgroundColor = Constants.EntryBackgroundColor;
            this.BirthdateDatePicker.BackgroundColor = Constants.EntryBackgroundColor;
            this.GoalPicker.BackgroundColor = Constants.EntryBackgroundColor;
            this.GenderPicker.BackgroundColor = Constants.EntryBackgroundColor;
            this.WeightEntry.BackgroundColor = Constants.EntryBackgroundColor;
            this.HeightEntry.BackgroundColor = Constants.EntryBackgroundColor;

            this.UsernameEntry.Completed += (s, e) => this.EmailEntry.Focus();
            this.EmailEntry.Completed += (s, e) => this.PasswordEntry.Focus();
            this.PasswordEntry.Completed += (s, e) => this.ConfirmPasswordEntry.Focus();

            this.ActivitySpinner.IsVisible = false;
        }

    }
}