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
        private DateTime displayDate;
        private float carbohydrateGoal;
        private float fatGoal;
        private float proteinGoal;

        public FoodViewModel()
        {
            this.DisplayDate = DateTime.Today;

            this.DisplayNextDate = new Command(this.OnDisplayNextDate);
            this.DisplayPrevDate = new Command(this.OnDisplayPrevDate);
        }

        public DateTime DisplayDate
        {
            get => displayDate;
            set
            {
                displayDate = value;
                this.OnPropertyChanged(nameof(this.DisplayDate));
            }
        }

        public float CarbohydrateGoal
        {
            get => carbohydrateGoal;
            set
            {
                carbohydrateGoal = value;
                this.OnPropertyChanged(nameof(this.CarbohydrateGoal));
            }
        }

        public float FatGoal
        {
            get => fatGoal;
            set
            {
                fatGoal = value;
                this.OnPropertyChanged(nameof(this.FatGoal));
            }
        }

        public float ProteinGoal
        {
            get => proteinGoal;
            set
            {
                proteinGoal = value;
                this.OnPropertyChanged(nameof(this.ProteinGoal));
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


        private void OnDisplayPrevDate()
        {
            this.DisplayDate = this.DisplayDate.AddDays(-1);
        }

        private void OnDisplayNextDate()
        {
            this.DisplayDate = this.DisplayDate.AddDays(1);
        }
    }
}
