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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddFoodPage : ContentPage
    {
        private readonly MealGroup mealGroup;

        public AddFoodPage()
        {
            InitializeComponent();
            this.Init();
        }

        public AddFoodPage(MealGroup mealGroup)
            : this()
        {
            this.mealGroup = mealGroup;
        }


        private void Init()
        {
            var vm = new AddFoodViewModel(this.mealGroup);
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