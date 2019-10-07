namespace MobileFitness.App.Views
{
    using MobileFitness.App.Controllers.Contracts;
    using MobileFitness.App.ViewModels;
    using System;
    using System.Threading.Tasks;

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

            this.UsernameEntry.BackgroundColor = Constants.EntryBackgroundColor;
            this.EmailEntry.BackgroundColor = Constants.EntryBackgroundColor;
            this.PasswordEntry.BackgroundColor = Constants.EntryBackgroundColor;

            this.UsernameEntry.Completed += (s, e) => this.EmailEntry.Focus();
            this.EmailEntry.Completed += (s, e) => this.PasswordEntry.Focus();
            this.PasswordEntry.Completed += (s, e) => this.RegisterButtonClicked(s, e);

            this.ActivitySpinner.IsVisible = false;
        }

        private async void RegisterButtonClicked(object sender, EventArgs e)
        {
            var userDto = new UserDto()
            {
                Username = this.UsernameEntry.Text,
                Email = this.EmailEntry.Text,
                Password = this.PasswordEntry.Text
            };

            var message = await this.userController.Register(userDto);

            await DisplayAlert("Login", message, "Ok");
        }
    }
}