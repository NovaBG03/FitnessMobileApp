using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
        private int genderIndex;
        private int goalIndex;

        public AccountViewModel()
        {
            this.context = DependencyService.Get<MobileFitnessContext>();

            this.Genders = new ObservableCollection<Gender>(Enum.GetValues(typeof(Gender))
                .OfType<Gender>()
                .ToList());

            this.Goals = new ObservableCollection<Goal>(Enum.GetValues(typeof(Goal))
                .OfType<Goal>()
                .ToList());
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

        public int GenderIndex
        {
            get => genderIndex;
            set
            {
                genderIndex = value;
                this.OnPropertyChanged(nameof(this.GenderIndex));
            }
        }

        public int GoalIndex
        {
            get => goalIndex;
            set
            {
                goalIndex = value;
                this.OnPropertyChanged(nameof(this.GoalIndex));
            }
        }

        public ObservableCollection<Gender> Genders { get; }

        public ObservableCollection<Goal> Goals { get; }

        public void SetNewUser(User user)
        {
            this.user = user;

            this.UpdateUserWeight();

            this.OnPropertyChanged(nameof(this.Username));
            this.OnPropertyChanged(nameof(this.Email));
            this.GenderIndex = (int)user.Gender;
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
    }
}
