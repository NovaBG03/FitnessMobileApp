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

    /// <summary>
    /// Страница за хранения
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MealPage : ContentPage
    {
        /// <summary>
        /// Създава нова страница
        /// </summary>
        public MealPage()
        {
            InitializeComponent();
            this.Init();
        }

        /// <summary>
        /// Инициализира ViewModel за страницата
        /// </summary>
        private void Init()
        {
            var vm = new MealViewModel();
            this.BindingContext = vm;
            vm.DisplayInvalidPrompt += (message) => DisplayAlert("Warning", message, "Ok");
            MessagingCenter.Subscribe<LoginViewModel, User>(vm, "ReloadUserInfo", (s, u) => vm.SetNewUser(u));
            MessagingCenter.Subscribe<AddFoodViewModel, object[]>(vm, "SaveFood", (s, args) => vm.SaveFood(args));
            MessagingCenter.Subscribe<AccountViewModel>(vm, "NewMacronutrients", (s) => vm.UpdateMacronutrientGoals());
        }

        /// <summary>
        /// Изпълнява командата за добавяне на храна
        /// </summary>
        /// <param name="sender">Подател</param>
        /// <param name="e">Аргументи</param>
        private void AddFoodCommandClicked(object sender, EventArgs e)
        {
            var mealName = ((Button)sender).Text.Replace("Add Food to ", "");

            var vm = (MealViewModel)this.BindingContext;
            vm.AddFood.Execute(mealName);
        }

        /// <summary>
        /// Изпълнява командата за премахване на храна
        /// </summary>
        /// <param name="sender">Подател</param>
        /// <param name="e">Аргументи</param>
        private void RemoveMealCommandClicked(object sender, EventArgs e)
        {
            var mealName = ((Button)sender).Text.Replace("Remove ", "");

            var vm = (MealViewModel)this.BindingContext;
            vm.RemoveMeal.Execute(mealName);
        }

        /// <summary>
        /// Изпълнява командата за показване на информацията на дадена храна
        /// </summary>
        /// <param name="sender">Подател</param>
        /// <param name="e">Аргументи</param>
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