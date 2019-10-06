using MobileFitness.App.Controllers;
using MobileFitness.App.Controllers.Contracts;
using MobileFitness.App.Views;
using MobileFitness.Data;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileFitness.App
{
    public partial class App : Application
    {
        private static IUserController userController;

        public static IUserController UserController 
            => userController ?? new UserController(); 


        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
