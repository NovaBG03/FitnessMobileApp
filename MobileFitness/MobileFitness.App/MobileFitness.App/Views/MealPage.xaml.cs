using System;
using System.Collections.Generic;
namespace MobileFitness.App.Views
{
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MobileFitness.App.Utils;
    using MobileFitness.App.ViewModels;
    using MobileFitness.Models;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MealPage : ContentPage
    {
        public MealPage()
        {
            InitializeComponent();
            this.Init();
        }

        private void Init()
        {
            var vm = new MealViewModel();
            this.BindingContext = vm;
            vm.DisplayInvalidPrompt += (message) => DisplayAlert("Warning", message, "Ok");
            MessagingCenter.Subscribe<LoginViewModel, User>(vm, "ReloadUserInfo", (s, u) => vm.SetNewUser(u));
            MessagingCenter.Subscribe<AddFoodViewModel, object[]>(vm, "SaveFood", (s, args) => vm.SaveFood(args));
            MessagingCenter.Subscribe<AccountViewModel>(vm, "NewMacronutrients", (s) => vm.UpdateMacronutrientGoals());
        }

        private void AddFoodCommandClicked(object sender, EventArgs e)
        {
            var mealName = ((Button)sender).Text.Replace("Add Food to ", "");

            var vm = (MealViewModel)this.BindingContext;
            vm.AddFood.Execute(mealName);
        }

        private void RemoveMealCommandClicked(object sender, EventArgs e)
        {
            var mealName = ((Button)sender).Text.Replace("Remove ", "");

            var vm = (MealViewModel)this.BindingContext;
            vm.RemoveMeal.Execute(mealName);
        }

        private void MealGroupsListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var listView = (ListView)sender;
            var selectedFood = (Food)e.SelectedItem;
            
            if (selectedFood == null)
            {
                return;
            }

            var vm = (MealViewModel)this.BindingContext;
            vm.ShowFoodInfo.Execute(selectedFood);

            listView.SelectedItem = null;
        }
    }
}