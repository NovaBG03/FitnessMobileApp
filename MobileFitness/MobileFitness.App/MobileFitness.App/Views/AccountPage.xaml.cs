using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileFitness.App.ViewModels;
using MobileFitness.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

/// <summary>
/// Страниците на приложениято
/// </summary>
namespace MobileFitness.App.Views
{
    /// <summary>
    /// Страница за профила на потребител
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountPage : ContentPage
    {
        /// <summary>
        /// Създава нова страница
        /// </summary>
        public AccountPage()
        {
            InitializeComponent();
            this.Init();
        }

        /// <summary>
        /// Инициализира ViewModel за страницата
        /// </summary>
        private void Init()
        {
            var vm = new AccountViewModel();
            this.BindingContext = vm;
            vm.DisplayInvalidPrompt += (message) => DisplayAlert("Warning", message, "Ok");
            MessagingCenter.Subscribe<LoginViewModel, User>(vm, "ReloadUserInfo", (s, u) => vm.SetNewUser(u));
            MessagingCenter.Subscribe<WeightViewModel>(vm, "UpdateUserWeight", (s) => vm.UpdateUserWeight());
        }
    }
}