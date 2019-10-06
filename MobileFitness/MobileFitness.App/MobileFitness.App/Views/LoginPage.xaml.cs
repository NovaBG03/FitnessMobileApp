namespace MobileFitness.App.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    using MobileFitness.App.ViewModels;
    using MobileFitness.App.Controllers.Contracts;
    using MobileFitness.Models;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly IUserController userController;

        public LoginPage()
        {
            InitializeComponent();
            Init();

            this.userController = App.UserController;
        }

        private void Init()
        {
            this.BackgroundColor = Constants.BackgroundColor;

            this.EmailLbl.TextColor = Constants.MainTextColor;
            this.PasswordLbl.TextColor = Constants.MainTextColor;

            this.ActivitySpinner.IsVisible = false;

            this.LoginIcon.HeightRequest = Constants.LoginIconHeight;

            this.EmailEntry.Completed += (s, e) => this.PasswordEntry.Focus();
            this.PasswordEntry.Completed += (s, e) => this.SignInButtonClicked(s, e);
        }

        private async void SignInButtonClicked(object sender, EventArgs e)
        {
            var userDto = new UserDto()
            {
                Email = this.EmailEntry.Text,
                Password = this.PasswordEntry.Text
            };

            var message = await this.userController.Login(userDto);

            try
            {
                var user = JsonConvert.DeserializeObject<UserDto>(message);

                await this.Navigation.PushAsync(new MainPage(user));
            }
            catch (Exception)
            {
                await DisplayAlert("Login", message, "Ok");
            }
            
        }

        private async void CreateAccountButtonClicked(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new RegisterPage());
        }
    }
}