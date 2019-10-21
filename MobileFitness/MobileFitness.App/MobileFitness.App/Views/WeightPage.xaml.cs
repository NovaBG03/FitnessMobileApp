using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileFitness.App.ViewModels;
using MobileFitness.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileFitness.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeightPage : ContentPage
    {
        public WeightPage()
        {
            InitializeComponent();
            this.Init();
        }

        private void Init()
        {
            var vm = new WeightViewModel();
            this.BindingContext = vm;
            vm.DisplayInvalidPrompt += (message) => DisplayAlert("Warning", message, "Ok");
            MessagingCenter.Subscribe<LoginViewModel, User>(vm, "ReloadUserInfo", (s, u) => vm.SetNewUser(u));
        }
    }
}