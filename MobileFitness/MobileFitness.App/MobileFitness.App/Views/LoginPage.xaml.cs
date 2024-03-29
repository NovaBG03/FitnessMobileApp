﻿namespace MobileFitness.App.Views
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

    /// <summary>
    /// Страница за влизане в профил
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        /// <summary>
        /// Създава нова страница
        /// </summary>
        public LoginPage()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// Инициализира ViewModel за страницата
        /// </summary>
        private void Init()
        {
            var vm = new LoginViewModel();
            this.BindingContext = vm;
            vm.DisplayInvalidPrompt += (message) => DisplayAlert("Login", message, "Ok");

            this.BackgroundColor = Constants.BarBackgroundColor;

            this.LoginIcon.HeightRequest = Constants.LoginIconHeight;

            this.EmailLbl.TextColor = Constants.MainTextColor;
            this.PasswordLbl.TextColor = Constants.MainTextColor;

            this.EmailEntry.BackgroundColor = Constants.EntryBackgroundColor;
            this.PasswordEntry.BackgroundColor = Constants.EntryBackgroundColor;

            this.EmailEntry.Completed += (s, e) => this.PasswordEntry.Focus();
            this.PasswordEntry.Completed += (s, e) => this.SignInButton.Command.Execute(null);

            this.ActivitySpinner.IsVisible = false;
        }
    }
}