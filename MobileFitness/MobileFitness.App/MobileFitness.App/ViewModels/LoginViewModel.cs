namespace MobileFitness.App.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Windows.Input;
    using Microsoft.EntityFrameworkCore;
    using MobileFitness.App.Utils;
    using MobileFitness.App.Views;
    using MobileFitness.Data;
    using Xamarin.Forms;

    /// <summary>
    /// ViewModel за вход в профил
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {
        private readonly MobileFitnessContext context;

        private string email;
        private string password;

        /// <summary>
        /// Създава нов ViewModel
        /// </summary>
        public LoginViewModel()
        {
            this.context = DependencyService.Get<MobileFitnessContext>();

            this.SingInCommand = new Command(this.OnSingIn);
            this.CreateAccountCommand = new Command(this.OnCreateAccount);
        }

        /// <summary>
        /// Електронна поща на потребителя
        /// </summary>
        public string Email
        {
            get => email;
            set
            {
                email = value?.ToLower();
                this.OnPropertyChanged(nameof(this.Email));
            }
        }

        /// <summary>
        /// Парола на потребителя
        /// </summary>
        public string Password
        {
            get => password;
            set
            {
                password = value;
                this.OnPropertyChanged(nameof(this.Password));
            }
        }

        /// <summary>
        /// Команда за влизане в профил
        /// </summary>
        public ICommand SingInCommand { get; private set; }

        /// <summary>
        /// Команда за отваряне на страница за създаване на нов профил
        /// </summary>
        public ICommand CreateAccountCommand { get; private set; }

        /// <summary>
        /// Влиза в профил
        /// </summary>
        private void OnSingIn()
        {
            var user = this.context.Users
                .Where(u => u.Email == this.Email)
                .FirstOrDefault();

            if (user == null
                || string.IsNullOrEmpty(this.Password)
                || string.IsNullOrWhiteSpace(this.Password))
            {
                this.DisplayInvalidLoginPrompt();
                return;
            }

            var hashedPassword = Convert.ToBase64String(
                 Common.SaltHashPassword(
                    Encoding.ASCII.GetBytes(password),
                    Convert.FromBase64String(user.Salt)));

            if (hashedPassword != user.Password)
            {
                this.DisplayInvalidLoginPrompt();
                return;
            }

            App.Current.MainPage = new NavigationPage(new MainPage());
            MessagingCenter.Send(this, "ReloadUserInfo", user);
        }

        /// <summary>
        /// Отваря страница за създаване на нов профил
        /// </summary>
        private void OnCreateAccount()
        {
            App.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }

        /// <summary>
        /// Показва греашка при опит за вход в профил
        /// </summary>
        private void DisplayInvalidLoginPrompt()
        {
            this.DisplayInvalidPrompt("Wrong Email or Password!");
        }
    }
}
