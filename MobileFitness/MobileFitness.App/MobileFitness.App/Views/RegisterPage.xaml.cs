namespace MobileFitness.App.Views
{
    using System;
    using System.Threading.Tasks;

    using MobileFitness.App.Controllers.Contracts;
    using MobileFitness.App.ViewModels;
    using Newtonsoft.Json;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private IUserController userController;

        public RegisterPage()
        {
            InitializeComponent();
            Init();

            this.userController = App.UserController;
        }

        private void Init()
        {
            this.BackgroundColor = Constants.BarBackgroundColor;

            this.UsernameLbl.TextColor = Constants.MainTextColor;
            this.EmailLbl.TextColor = Constants.MainTextColor;
            this.PasswordLbl.TextColor = Constants.MainTextColor;
            this.ConfirmPasswordLbl.TextColor = Constants.MainTextColor;
            this.BirthdateLbl.TextColor = Constants.MainTextColor;
            this.GenderLbl.TextColor = Constants.MainTextColor;
            this.WeightLbl.TextColor = Constants.MainTextColor;
            this.HeightLbl.TextColor = Constants.MainTextColor;

            this.UsernameEntry.BackgroundColor = Constants.EntryBackgroundColor;
            this.EmailEntry.BackgroundColor = Constants.EntryBackgroundColor;
            this.PasswordEntry.BackgroundColor = Constants.EntryBackgroundColor;
            this.ConfirmPasswordEntry.BackgroundColor = Constants.EntryBackgroundColor;
            this.BirthdateDatePicker.BackgroundColor = Constants.EntryBackgroundColor;
            this.GenderPicker.BackgroundColor = Constants.EntryBackgroundColor;
            this.WeightEntry.BackgroundColor = Constants.EntryBackgroundColor;
            this.HeightEntry.BackgroundColor = Constants.EntryBackgroundColor;

            this.UsernameEntry.Completed += (s, e) => this.EmailEntry.Focus();
            this.EmailEntry.Completed += (s, e) => this.PasswordEntry.Focus();
            this.PasswordEntry.Completed += (s, e) => this.ConfirmPasswordEntry.Focus();

            this.ActivitySpinner.IsVisible = false;
        }

        private async void RegisterButtonClicked(object sender, EventArgs e)
        {
            var userToRegister = new UserToRegister()
            {
                Username = this.UsernameEntry.Text,
                Email = this.EmailEntry.Text,
                Password = this.PasswordEntry.Text,
                ConfirmPassword = this.ConfirmPasswordEntry.Text,
                Birthdate = this.BirthdateDatePicker.Date,
                Gender = this.GenderPicker.SelectedIndex,
            };

            if (float.TryParse(this.WeightEntry.Text != null ? 
                    this.WeightEntry.Text.Replace(",", ".") : 
                    default(float).ToString(),
                out float weight))
            {
                userToRegister.Weight = weight;
            }


            //TODO add height in db
            //TODO do the same for HeightEntry

            await DisplayAlert("Login", JsonConvert.SerializeObject(userToRegister), "Ok");
            return;

            var message = await this.userController.Register(userToRegister);

            await DisplayAlert("Login", message, "Ok");
        }
    }
}