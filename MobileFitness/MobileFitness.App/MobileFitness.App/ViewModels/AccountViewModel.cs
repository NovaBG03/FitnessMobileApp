using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MobileFitness.App.Utils;
using MobileFitness.Data;
using MobileFitness.Models;
using MobileFitness.Models.Enums;
using Xamarin.Forms;

namespace MobileFitness.App.ViewModels
{
    public class AccountViewModel : BaseViewModel
    {
        private readonly MobileFitnessContext context;

        private User user;
        private float weightInKilograms;
        private int goalIndex;
        private float carbohydrateGoal;
        private float fatGoal;
        private float proteinGoal;

        public AccountViewModel()
        {
            this.context = DependencyService.Get<MobileFitnessContext>();

            this.Goals = new ObservableCollection<Goal>(Enum.GetValues(typeof(Goal))
                .OfType<Goal>()
                .ToList());

            this.UpdateMacronutrients = new Command(this.OnUpdateMacronutrients);
        }

        public float WeightInKilograms
        {
            get => weightInKilograms;
            set
            {
                weightInKilograms = value;
                this.OnPropertyChanged(nameof(this.WeightInKilograms));
            }
        }

        public string Username 
            => this.user?.Username ?? null;

        public string Email 
            => this.user?.Email ?? null;

        public int GoalIndex
        {
            get => goalIndex;
            set
            {
                goalIndex = value;
                this.OnPropertyChanged(nameof(this.GoalIndex));
                this.UpdateUserGoal();
            }
        }
        public float CarbohydrateGoal
        {
            get => carbohydrateGoal;
            set
            {
                carbohydrateGoal = value;
                this.OnPropertyChanged(nameof(this.CarbohydrateGoal));
                this.OnPropertyChanged(nameof(this.CaloriesGoal));
            }
        }

        public float FatGoal
        {
            get => fatGoal;
            set
            {
                fatGoal = value;
                this.OnPropertyChanged(nameof(this.FatGoal));
                this.OnPropertyChanged(nameof(this.CaloriesGoal));
            }
        }

        public float ProteinGoal
        {
            get => proteinGoal;
            set
            {
                proteinGoal = value;
                this.OnPropertyChanged(nameof(this.ProteinGoal));
                this.OnPropertyChanged(nameof(this.CaloriesGoal));
            }
        }

        public float CaloriesGoal
            => (this.ProteinGoal * 4) + (this.CarbohydrateGoal * 4) + (this.FatGoal * 9);

        public ObservableCollection<Goal> Goals { get; }

        public ICommand UpdateMacronutrients { get; set; }

        private void UpdateUserGoal()
        {
            if (this.user == null)
            {
                return;
            }

            this.user.Goal = (Goal)this.GoalIndex;
            this.context.SaveChanges();
        }

        public void SetNewUser(User user)
        {
            this.user = user;

            this.UpdateUserWeight();

            this.OnPropertyChanged(nameof(this.Username));
            this.OnPropertyChanged(nameof(this.Email));
            this.GoalIndex = (int)user.Goal;
        }

        public void UpdateUserWeight()
        {
            var weight = this.context
                .Weights
                .Where(w => w.User == user)
                .OrderByDescending(w => w.Date)
                .First();

            this.WeightInKilograms = weight.Kilograms;
        }

        private void OnUpdateMacronutrients()
        {
            MacronutrientManager.SetNewMacronutrientGoal(user);

            this.context.SaveChanges();

            MessagingCenter.Send(this, "NewMacronutrients");
        }
    }
}
