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

/// <summary>
/// ViewModel-ите на приложението
/// </summary>
namespace MobileFitness.App.ViewModels
{
    /// <summary>
    /// ViewModel за профила на потребителя
    /// </summary>
    public class AccountViewModel : BaseViewModel
    {
        private readonly MobileFitnessContext context;

        private User user;
        private float weightInKilograms;
        private int goalIndex;
        private float carbohydrateGoal;
        private float fatGoal;
        private float proteinGoal;
        
        /// <summary>
        /// Създава нов ViewModel
        /// </summary>
        public AccountViewModel()
        {
            this.context = DependencyService.Get<MobileFitnessContext>();

            this.Goals = new ObservableCollection<Goal>(Enum.GetValues(typeof(Goal))
                .OfType<Goal>()
                .ToList());

            this.UpdateMacronutrients = new Command(this.OnUpdateMacronutrients);
        }
        
        /// <summary>
        /// Теглото в килограми на потребителя
        /// </summary>
        public float WeightInKilograms
        {
            get => weightInKilograms;
            set
            {
                weightInKilograms = value;
                this.OnPropertyChanged(nameof(this.WeightInKilograms));
            }
        }

        /// <summary>
        /// Потребителско име на потребителя
        /// </summary>
        public string Username 
            => this.user?.Username ?? null;

        /// <summary>
        /// Електронна поща на потребителя
        /// </summary>
        public string Email 
            => this.user?.Email ?? null;

        /// <summary>
        /// Индех на целта на потребителя
        /// </summary>
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

        /// <summary>
        /// Цел за въглехидрати на потребителя
        /// </summary>
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


        /// <summary>
        /// Цел за мазнини на потребителя
        /// </summary>
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

        /// <summary>
        /// Цел за белтъци на потребителя
        /// </summary>
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

        /// <summary>
        /// Калориини цели на потребителя
        /// </summary>
        public float CaloriesGoal
            => (this.ProteinGoal * 4) + (this.CarbohydrateGoal * 4) + (this.FatGoal * 9);

        /// <summary>
        /// Възможни цели
        /// </summary>
        public ObservableCollection<Goal> Goals { get; }

        /// <summary>
        /// Команда за актуализация на макронутриентите
        /// </summary>
        public ICommand UpdateMacronutrients { get; set; }

        /// <summary>
        /// Актуализация на целите на потребителя
        /// </summary>
        private void UpdateUserGoal()
        {
            if (this.user == null)
            {
                return;
            }

            this.user.Goal = (Goal)this.GoalIndex;
            this.context.SaveChanges();
        }

        /// <summary>
        /// Задава нов потребител на ViewModel-а
        /// </summary>
        /// <param name="user">Потребител</param>
        public void SetNewUser(User user)
        {
            this.user = user;

            this.UpdateUserWeight();

            this.OnPropertyChanged(nameof(this.Username));
            this.OnPropertyChanged(nameof(this.Email));
            this.GoalIndex = (int)user.Goal;
        }

        /// <summary>
        /// Актуализация на теглото на потребителя
        /// </summary>
        public void UpdateUserWeight()
        {
            var weight = this.context
                .Weights
                .Where(w => w.User == user)
                .OrderByDescending(w => w.Date)
                .First();

            this.WeightInKilograms = weight.Kilograms;
        }

        /// <summary>
        /// Актуализация на макронутриентите
        /// </summary>
        private void OnUpdateMacronutrients()
        {
            MacronutrientManager.SetNewMacronutrientGoal(user);

            this.context.SaveChanges();

            MessagingCenter.Send(this, "NewMacronutrients");
        }
    }
}
