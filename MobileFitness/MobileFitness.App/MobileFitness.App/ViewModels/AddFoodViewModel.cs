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
    public class AddFoodViewModel : BaseViewModel
    {
        private readonly MobileFitnessContext context;
        private readonly MealGroup mealGroup;

        private float carbohydrate;
        private float fat;
        private float protein;
        private string searchingInput;
        private float foodQuantity;
        private Food selectedFood;

        public AddFoodViewModel()
        {

        }

        public AddFoodViewModel(MealGroup mealGroup)
        {
            this.mealGroup = mealGroup;

            this.UpdateFoundedFoods = new Command(this.OnUpdateFoundedFoods);

            this.context = DependencyService.Get<MobileFitnessContext>();
            this.FoundedFoods = new ObservableCollection<Food>();

            this.UpdateSelectedFoodInfo();
        }

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

        public float CaloriesGoal
            => (this.ProteinGoal * 4) + (this.CarbohydrateGoal * 4) + (this.FatGoal * 9);

        public string SearchingInput
        {
            get => searchingInput;
            set
            {
                searchingInput = value;
                this.OnPropertyChanged(nameof(this.SearchingInput));
            }
        }

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

        public ObservableCollection<Food> FoundedFoods { get; set; }

        public ICommand UpdateFoundedFoods { get; set; }

        private void OnUpdateFoundedFoods()
        {
            this.FoundedFoods.Clear();

            var inputToLower = this.SearchingInput?.ToLower();

            var foundedFoods = this.context
                .Foods
                .Include(f => f.Macronutrient)
                .Where(f => f.Name.ToLower().Contains(inputToLower))
                .OrderBy(f => f.Name)
                .Take(50)
                .ToArray();

            foreach (var food in foundedFoods)
            {
                this.FoundedFoods.Add(food);
            }
        }

        private void ReserFoodQuantity()
        {
            this.FoodQuantity = 100;
        }
    }
}
