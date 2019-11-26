using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileFitness.App.Utils;
using MobileFitness.App.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileFitness.App.Views
{
    /// <summary>
    /// Страница за добавяне на храна
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddFoodPage : ContentPage
    {
        /// <summary>
        /// Създава нова страница
        /// </summary>
        public AddFoodPage()
        {
            InitializeComponent();
            this.Init();
        }

        /// <summary>
        /// Инициализира ViewModel за страницата
        /// </summary>
        private void Init()
        {
            var vm = new AddFoodViewModel();
            this.BindingContext = vm;
            vm.DisplayInvalidPrompt += (message) => DisplayAlert("Warning", message, "Ok");
        }

        private void SearchBarEntryUnfocused(object sender, FocusEventArgs e)
        {
            var vm = (AddFoodViewModel)this.BindingContext;
            vm.UpdateFoundedFoods.Execute(null);
        }
    }
}