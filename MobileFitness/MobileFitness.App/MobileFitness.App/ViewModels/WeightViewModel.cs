using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MobileFitness.Data;
using MobileFitness.Models;
using Xamarin.Forms;

namespace MobileFitness.App.ViewModels
{
    /// <summary>
    /// ViewModel за тегло на потребителя
    /// </summary>
    public class WeightViewModel : BaseViewModel
    {
        private readonly MobileFitnessContext context;

        private User user;
        private DateTime date;
        private float weightInKilograms;

        /// <summary>
        /// Създава нов ViewModel
        /// </summary>
        public WeightViewModel()
        {
            this.context = DependencyService.Get<MobileFitnessContext>();

            this.Weights = new ObservableCollection<Weight>();

            this.AddWeight = new Command(OnAddWeight);

            this.Date = DateTime.Today;
        }

        /// <summary>
        /// Избрана дата
        /// </summary>
        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                this.OnPropertyChanged(nameof(this.Date));
            }
        }

        /// <summary>
        /// Тегло в килограми
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
        /// Колекция от всияки записи на теглото
        /// </summary>
        public ObservableCollection<Weight> Weights { get; }

        /// <summary>
        /// Команда за записване на тегло
        /// </summary>
        public ICommand AddWeight { get; set; }

        /// <summary>
        /// Задава нов потребител на ViewModel
        /// </summary>
        /// <param name="user"></param>
        public void SetNewUser(User user)
        {
            this.user = user;
            this.UpdateShownWeights();
        }

        /// <summary>
        /// Актуализация на показаните записи
        /// </summary>
        private void UpdateShownWeights()
        {
            this.Weights.Clear();

            var weights = this.context
                .Weights
                .Where(w => w.User == user)
                .OrderByDescending(w => w.Date)
                .Take(50)
                .ToArray();

            foreach (var weight in weights)
            {
                this.Weights.Add(weight);
            }
        }

        /// <summary>
        /// Записва ново тегло
        /// </summary>
        private void OnAddWeight()
        {
            if (this.WeightInKilograms > 300 || this.WeightInKilograms < 20)
            {
                this.DisplayInvalidPrompt("Please enter correct Weight in kilograms!");
                return;
            }

            var weight = this.context.Weights
                    .FirstOrDefault(w => w.User == this.user 
                                      && w.Date == this.Date);

            if (weight == null)
            {
                this.context.Weights.Add(new Weight()
                {
                    Date = this.Date,
                    Kilograms = this.WeightInKilograms,
                    User = this.user
                });
            }
            else
            {
                weight.Kilograms = this.WeightInKilograms;
            }

            this.context.SaveChanges();

            this.UpdateShownWeights();
            MessagingCenter.Send(this, "UpdateUserWeight");
        }
    }
}
