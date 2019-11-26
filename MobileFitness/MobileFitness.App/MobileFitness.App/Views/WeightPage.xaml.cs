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
    /// <summary>
    /// Страница за тегло
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeightPage : ContentPage
    {
        /// <summary>
        /// Създава нова страница
        /// </summary>
        public WeightPage()
        {
            InitializeComponent();
            this.Init();
        }

        /// <summary>
        /// Инициализира ViewModel за страницата
        /// </summary>
        private void Init()
        {
            var vm = new WeightViewModel();
            this.BindingContext = vm;
            vm.DisplayInvalidPrompt += (message) => DisplayAlert("Warning", message, "Ok");
            MessagingCenter.Subscribe<LoginViewModel, User>(vm, "ReloadUserInfo", (s, u) => vm.SetNewUser(u));
        }
    }
}