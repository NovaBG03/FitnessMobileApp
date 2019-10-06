namespace MobileFitness.App.Views
{
    using MobileFitness.App.ViewModels;
    using System;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private UserDto userDto;

        public MainPage(UserDto userDto)
        {
            InitializeComponent();

            this.userDto = userDto;
        }
    }
}