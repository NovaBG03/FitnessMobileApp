using System;
using System.Collections.Generic;
namespace MobileFitness.App.Views
{
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MobileFitness.App.ViewModels;
    using MobileFitness.Models;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodPage : ContentPage
    {
        public FoodPage()
        {
            InitializeComponent();
            this.Init();
        }

        private void Init()
        {
            var vm = new FoodViewModel();
            this.BindingContext = vm;
            vm.DisplayInvalidPrompt += (message) => DisplayAlert("Warning", message, "Ok");
            MessagingCenter.Subscribe<LoginViewModel, User>(vm, "ReloadUserInfo", (s, u) => vm.SetNewUser(u));
        }
    }
}