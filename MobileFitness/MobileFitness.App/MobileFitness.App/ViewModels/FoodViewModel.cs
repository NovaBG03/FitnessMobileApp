namespace MobileFitness.App.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Net.Mail;
    using System.Text;
    using System.Windows.Input;
    using Microsoft.EntityFrameworkCore;
    using MobileFitness.App.Utils;
    using MobileFitness.App.Views;
    using MobileFitness.Data;
    using MobileFitness.Models;
    using MobileFitness.Models.Enums;
    using Xamarin.Forms;

    public class FoodViewModel : BaseViewModel
    {
        private readonly MobileFitnessContext context;

        private User user;
        private DateTime displayDate;
        private float carbohydrateGoal;
        private float fatGoal;
        private float proteinGoal;
        private float carbohydrateUsed;
        private float fatUsed;
        private float proteinUsed;

        public FoodViewModel()
        {
            this.context = DependencyService.Get<MobileFitnessContext>();

            this.MealGroups = new ObservableCollection<MealGroup>();

            this.DisplayDate = DateTime.Today;

            this.DisplayNextDate = new Command(this.OnDisplayNextDate);
            this.DisplayPrevDate = new Command(this.OnDisplayPrevDate);
            this.AddMeal = new Command(this.OnAddMeal);
            this.RemoveMeal = new Command<string>(this.OnRemoveMeal);
            this.AddFood = new Command<string>(this.OnAddFood);
        }

        public DateTime DisplayDate
        {
            get => displayDate;
            set
            {
                displayDate = value;
                this.OnPropertyChanged(nameof(this.DisplayDate));

                this.UpdateDisplayedMeals();
                this.UpdateDisplayedMacronutrients();
                this.UpdateUsedMacronutrients();
            }
        }

        public float CarbohydrateGoal
        {
            get => carbohydrateGoal;
            set
            {
                carbohydrateGoal = value;
                this.OnPropertyChanged(nameof(this.CarbohydrateGoal));
                this.OnPropertyChanged(nameof(this.CarbohydrateLeft));
                this.OnPropertyChanged(nameof(this.CaloriesGoal));
                this.OnPropertyChanged(nameof(this.CaloriesLeft));
            }
        }

        public float FatGoal
        {
            get => fatGoal;
            set
            {
                fatGoal = value;
                this.OnPropertyChanged(nameof(this.FatGoal));
                this.OnPropertyChanged(nameof(this.FatLeft));
                this.OnPropertyChanged(nameof(this.CaloriesGoal));
                this.OnPropertyChanged(nameof(this.CaloriesLeft));
            }
        }

        public float ProteinGoal
        {
            get => proteinGoal;
            set
            {
                proteinGoal = value;
                this.OnPropertyChanged(nameof(this.ProteinGoal));
                this.OnPropertyChanged(nameof(this.ProteinLeft));
                this.OnPropertyChanged(nameof(this.CaloriesGoal));
                this.OnPropertyChanged(nameof(this.CaloriesLeft));
            }
        }

        public float CaloriesGoal
            => (this.ProteinGoal * 4) + (this.CarbohydrateGoal * 4) + (this.FatGoal * 9);

        public float CarbohydrateUsed
        {
            get => carbohydrateUsed;
            set
            {
                carbohydrateUsed = value;
                this.OnPropertyChanged(nameof(this.CarbohydrateUsed));
                this.OnPropertyChanged(nameof(this.CarbohydrateLeft));
                this.OnPropertyChanged(nameof(this.CaloriesUsed));
                this.OnPropertyChanged(nameof(this.CaloriesLeft));
            }
        }

        public float FatUsed
        {
            get => fatUsed;
            set
            {
                fatUsed = value;
                this.OnPropertyChanged(nameof(this.FatUsed));
                this.OnPropertyChanged(nameof(this.FatLeft));
                this.OnPropertyChanged(nameof(this.CaloriesUsed));
                this.OnPropertyChanged(nameof(this.CaloriesLeft));
            }
        }

        public float CaloriesUsed
            => (this.ProteinUsed * 4) + (this.CarbohydrateUsed * 4) + (this.FatUsed * 9);

        public float ProteinUsed
        {
            get => proteinUsed;
            set
            {
                proteinUsed = value;
                this.OnPropertyChanged(nameof(this.ProteinUsed));
                this.OnPropertyChanged(nameof(this.ProteinLeft));
                this.OnPropertyChanged(nameof(this.CaloriesUsed));
                this.OnPropertyChanged(nameof(this.CaloriesLeft));
            }
        }

        public int CarbohydrateLeft
            => (int)(this.CarbohydrateGoal - this.CarbohydrateUsed);

        public int FatLeft
            => (int)(this.FatGoal - this.FatUsed);

        public int ProteinLeft
            => (int)(this.ProteinGoal - this.ProteinUsed);

        public int CaloriesLeft
            => (int)(this.CaloriesGoal - this.CaloriesUsed);

        public DateTime MaxDate
            => DateTime.Today.AddYears(1);

        public DateTime MinDate
            => new DateTime(2010, 1, 1);

        public ICommand DisplayNextDate { get; private set; }

        public ICommand DisplayPrevDate { get; private set; }

        public ICommand AddMeal { get; private set; }

        public ICommand RemoveMeal { get; private set; }

        public ICommand AddFood { get; private set; }

        public ObservableCollection<MealGroup> MealGroups { get; set; }

        public void SetNewUser(User user)
        {
            this.user = user;
            this.UpdateDisplayedMeals();
            this.UpdateDisplayedMacronutrients();
            this.UpdateUsedMacronutrients();
        }

        private void UpdateDisplayedMacronutrients()
        {
            if (this.user == null)
            {
                return;
            }

            var currentGoal = this.context
                .UsersMacronutrients
                .Where(um => um.UserId == this.user.Id
                    && um.Date <= this.DisplayDate)
                .OrderByDescending(um => um.Date)
                .Select(um => new
                {
                    um.Macronutrient.Carbohydrate,
                    um.Macronutrient.Fat,
                    um.Macronutrient.Protein
                })
                .FirstOrDefault();

            if (currentGoal == null)
            {
                currentGoal = this.context
                .UsersMacronutrients
                .Where(um => um.UserId == this.user.Id)
                .OrderBy(um => um.Date)
                .Select(um => new
                {
                    um.Macronutrient.Carbohydrate,
                    um.Macronutrient.Fat,
                    um.Macronutrient.Protein
                })
                .FirstOrDefault();
            }

            this.CarbohydrateGoal = currentGoal.Carbohydrate;
            this.FatGoal = currentGoal.Fat;
            this.ProteinGoal = currentGoal.Protein;
        }

        private void UpdateDisplayedMeals()
        {
            if (this.user == null)
            {
                return;
            }

            var meals = this.context
                .Meals
                .Include(m => m.MealsFoods)
                .Include(m => m.User)
                .OrderBy(m => m.Name)
                .Where(m => m.Date == this.DisplayDate
                    && m.User == this.user)
                .ToList();

            var mealsFoods = this.context
                .MealFoods
                .Include(mf => mf.Meal)
                .Include(mf => mf.Food)
                .ThenInclude(f => f.Macronutrient)
                .Where(mf => mf.Meal.Date == this.DisplayDate
                    && mf.Meal.User == this.user)
                .ToList();

            this.MealGroups.Clear();

            foreach (var meal in meals)
            {
                var currentMealGroup = new MealGroup()
                {
                    MealName = meal.Name
                };

                foreach (var food in meal.MealsFoods.Select(mf => mf.Food))
                {
                    currentMealGroup.Add(food);
                }

                this.MealGroups.Add(currentMealGroup);
            }
        }

        private void UpdateUsedMacronutrients()
        {
            this.CarbohydrateUsed = this.MealGroups.Sum(mg => mg.Foods.Sum(f => f.Macronutrient.Carbohydrate));
            this.FatUsed = this.MealGroups.Sum(mg => mg.Foods.Sum(f => f.Macronutrient.Fat));
            this.ProteinUsed = this.MealGroups.Sum(mg => mg.Foods.Sum(f => f.Macronutrient.Protein));
        }

        private void OnDisplayPrevDate()
        {
            this.DisplayDate = this.DisplayDate.AddDays(-1);
        }

        private void OnDisplayNextDate()
        {
            this.DisplayDate = this.DisplayDate.AddDays(1);
        }

        private void OnAddMeal()
        {
            var meal = new Meal()
            {

                Date = this.DisplayDate,
                User = this.user
            };

            int mealNumber = 1;
            do
            {
                meal.Name = $"Meal {mealNumber++}";
            } while (this.MealGroups.Any(mg => mg.MealName == meal.Name));

            this.context.Meals.Add(meal);
            this.context.SaveChanges();

            this.MealGroups.Add(new MealGroup()
            {
                MealName = meal.Name,
            });
        }

        private void OnRemoveMeal(string mealName)
        {
            var mealGroup = this.MealGroups.First(m => m.MealName == mealName);
            this.MealGroups.Remove(mealGroup);

            var meal = this.context.Meals
                .Where(m => m.Date == this.DisplayDate
                    && m.Name == mealName)
                .FirstOrDefault();

            this.context.Meals.Remove(meal);

            this.context.SaveChanges();

            this.UpdateUsedMacronutrients();
        }

        private void OnAddFood(string mealName)
        {
            //TODO CREATE ADD FOOD PAGE
            //App.Current.MainPage.Navigation.PushAsync(new AddFoodPage());
            //return;

            var food = new Food()
            {
                Name = "Food",
                Macronutrient = new Macronutrient()
                {
                    Carbohydrate = 25,
                    Fat = 1,
                    Protein = 10
                }
            };

            var mealGroup = this.MealGroups.First(m => m.MealName == mealName);
            mealGroup.Add(food);

            this.context.Meals
                .Where(m => m.Date == this.DisplayDate
                    && m.Name == mealName)
                .FirstOrDefault()
                .MealsFoods
                .Add(new MealFood()
                {
                    Food = food
                });
            this.context.SaveChanges();

            this.UpdateUsedMacronutrients();
        }
    }
}
