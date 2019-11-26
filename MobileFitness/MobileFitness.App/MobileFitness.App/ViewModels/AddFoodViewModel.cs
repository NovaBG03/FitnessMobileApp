using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using MobileFitness.App.Utils;
using MobileFitness.Data;
using MobileFitness.Models;
using Xamarin.Forms;

namespace MobileFitness.App.ViewModels
{
    /// <summary>
    /// ViewModel за добавяне на храна
    /// </summary>
    public class AddFoodViewModel : BaseViewModel
    {
        private readonly MobileFitnessContext context;

        private float carbohydrate;
        private float fat;
        private float protein;
        private string searchingInput;
        private float foodQuantity;
        private Food selectedFood;

        /// <summary>
        /// Създава нов ViewModel
        /// </summary>
        public AddFoodViewModel()
        {
            this.UpdateFoundedFoods = new Command(this.OnUpdateFoundedFoods);
            this.SaveFood = new Command(this.OnSaveFood);

            this.context = DependencyService.Get<MobileFitnessContext>();

            this.FoundedFoods = new ObservableCollection<Food>();
            this.SetDefaultFoundedFoods();

            this.UpdateSelectedFoodInfo();
        }

        private void SetDefaultFoundedFoods()
        {
            var foundedFoods = this.context
                .Foods
                .Include(f => f.Macronutrient)
                .OrderBy(f => f.Name)
                .Take(50)
                .ToArray();

            this.SetFoundedFoods(foundedFoods);
        }

        /// <summary>
        /// Въглехидрати на храната
        /// </summary>
        public float CarbohydrateGoal
        {
            get => carbohydrate;
            set
            {
                carbohydrate = value;
                this.OnPropertyChanged(nameof(this.CarbohydrateGoal));
                this.OnPropertyChanged(nameof(this.CaloriesGoal));
            }
        }

        /// <summary>
        /// Мазнини на храната
        /// </summary>
        public float FatGoal
        {
            get => fat;
            set
            {
                fat = value;
                this.OnPropertyChanged(nameof(this.FatGoal));
                this.OnPropertyChanged(nameof(this.CaloriesGoal));
            }
        }

        /// <summary>
        /// Белтъци на храната
        /// </summary>
        public float ProteinGoal
        {
            get => protein;
            set
            {
                protein = value;
                this.OnPropertyChanged(nameof(this.ProteinGoal));
                this.OnPropertyChanged(nameof(this.CaloriesGoal));
            }
        }

        /// <summary>
        /// Калории на храната
        /// </summary>
        public float CaloriesGoal
            => (this.ProteinGoal * 4) + (this.CarbohydrateGoal * 4) + (this.FatGoal * 9);

        /// <summary>
        /// Ключови думи за търсене на храна
        /// </summary>
        public string SearchingInput
        {
            get => searchingInput;
            set
            {
                searchingInput = value;
                this.OnPropertyChanged(nameof(this.SearchingInput));
            }
        }

        /// <summary>
        /// Тегло на храната в грамове
        /// </summary>
        public float FoodQuantity
        {
            get => foodQuantity;
            set
            {
                foodQuantity = value;
                this.OnPropertyChanged(nameof(this.FoodQuantity));
                this.UpdateSelectedFoodInfo();
            }
        }

        /// <summary>
        /// Избрана храна
        /// </summary>
        public Food SelectedFood
        {
            get => selectedFood;
            set
            {
                selectedFood = value;
                this.OnPropertyChanged(nameof(this.SelectedFood));
                this.ReserFoodQuantity();
                this.UpdateSelectedFoodInfo();
            }
        }

        /// <summary>
        /// Актуализация на информацията за избраната храна
        /// </summary>
        private void UpdateSelectedFoodInfo()
        {
            if (this.SelectedFood == null)
            {
                return;
            }

            this.CarbohydrateGoal = this.SelectedFood.Macronutrient.Carbohydrate * this.FoodQuantity / 100;
            this.FatGoal = this.SelectedFood.Macronutrient.Fat * this.FoodQuantity / 100;
            this.ProteinGoal = this.SelectedFood.Macronutrient.Protein * this.FoodQuantity / 100;
        }

        /// <summary>
        /// Намерени храни
        /// </summary>
        public ObservableCollection<Food> FoundedFoods { get; set; }

        /// <summary>
        /// Команда за актуализация на намерените храни
        /// </summary>
        public ICommand UpdateFoundedFoods { get; set; }

        /// <summary>
        /// Команда за записване на избраната храна
        /// </summary>
        public ICommand SaveFood { get; set; }

        /// <summary>
        /// Актуализация на намерените храни
        /// </summary>
        private void OnUpdateFoundedFoods()
        {
            this.FoundedFoods.Clear();

            var inputToLower = this.SearchingInput?.ToLower();

            if (string.IsNullOrEmpty(inputToLower) || string.IsNullOrWhiteSpace(inputToLower))
            {
                this.SetDefaultFoundedFoods();
                return;
            }

            var foundedFoods = this.context
                .Foods
                .Include(f => f.Macronutrient)
                .Where(f => f.Name.ToLower().Contains(inputToLower))
                .OrderBy(f => f.Name)
                .Take(50)
                .ToArray();

            this.SetFoundedFoods(foundedFoods);
        }

        /// <summary>
        /// Задаване на намерените храни
        /// </summary>
        /// <param name="foundedFoods">Намерени храни</param>
        private void SetFoundedFoods(Food[] foundedFoods)
        {
            foreach (var food in foundedFoods)
            {
                this.FoundedFoods.Add(food);
            }
        }

        /// <summary>
        /// Записва избраната храна
        /// </summary>
        private void OnSaveFood()
        {
            if(this.SelectedFood == null)
            {
                App.Current.MainPage.Navigation.PopAsync();
                return;
            }

            if (this.FoodQuantity < 1)
            {
                this.DisplayInvalidPrompt("Please enter correct Quantity.");
            }

            MessagingCenter.Send(this, "SaveFood", new object[] { this.SelectedFood, this.FoodQuantity });
            App.Current.MainPage.Navigation.PopAsync();
        }

        /// <summary>
        /// Нулира количеството избрана храна
        /// </summary>
        private void ReserFoodQuantity()
        {
            this.FoodQuantity = 100;
        }
    }
}
