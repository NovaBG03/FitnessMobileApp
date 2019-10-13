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
        private Meal selectedMeal;

        public FoodViewModel()
        {
            this.context = DependencyService.Get<MobileFitnessContext>();

            this.DisplayDate = DateTime.Today;

            this.DisplayNextDate = new Command(this.OnDisplayNextDate);
            this.DisplayPrevDate = new Command(this.OnDisplayPrevDate);
            this.AddMeal = new Command(this.OnAddMeal);
            this.AddFood = new Command(this.OnAddFood);

            this.Meals = new ObservableCollection<Meal>();
        }

        public DateTime DisplayDate
        {
            get => displayDate;
            set
            {
                displayDate = value;
                this.OnPropertyChanged(nameof(this.DisplayDate));
                this.UpdateDisplayedMacronutrients();
                this.UpdateDisplayedMeals();
            }
        }

        public Meal SelectedMeal
        {
            get => selectedMeal;
            set
            {
                selectedMeal = value;
                this.OnPropertyChanged(nameof(this.SelectedMeal));
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

        public int CarbohydrateLeft
            => (int)this.CarbohydrateGoal;

        public int FatLeft
            => (int)this.FatGoal;

        public int ProteinLeft
            => (int)this.ProteinGoal;

        public float CaloriesLeft
            => (this.ProteinLeft * 4) + (this.CarbohydrateLeft * 4) + (this.FatLeft * 9);

        public DateTime MaxDate
            => DateTime.Today.AddYears(1);

        public DateTime MinDate
            => new DateTime(2010, 1, 1);

        public ICommand DisplayNextDate { get; private set; }

        public ICommand DisplayPrevDate { get; private set; }

        public ICommand AddMeal { get; private set; }

        public ICommand AddFood { get; private set; }

        public ObservableCollection<Meal> Meals { get; private set; }

        public void SetNewUser(User user)
        {
            this.user = user;
            this.UpdateDisplayedMacronutrients();
            this.UpdateDisplayedMeals();
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
            if (this.Meals == null)
            {
                return;
            }

            var meals = this.context
                .Meals
                .Where(m => m.Date == this.DisplayDate)
                .ToList();

            this.Meals.Clear();

            foreach (var meal in meals)
            {
                this.Meals.Add(meal);
            }
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
                Name = $"Meal {this.Meals.Count + 1}",
                Date = this.DisplayDate,
                User = this.user
            };

            this.Meals.Add(meal);
            //TODO add this.context.SaveChanges();
        }

        private void OnAddFood()
        {
            App.Current.MainPage.Navigation.PushAsync(new AddFoodPage());
        }
    }
}
