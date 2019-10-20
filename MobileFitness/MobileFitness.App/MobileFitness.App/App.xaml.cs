using MobileFitness.App.ViewModels;
using MobileFitness.App.Views;
using MobileFitness.Data;
using MobileFitness.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileFitness.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var context = DependencyService.Get<MobileFitnessContext>();

            this.MainPage = new NavigationPage(new LoginPage());
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
