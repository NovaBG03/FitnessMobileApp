namespace MobileFitness.App.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Text;
    using MobileFitness.Models;

    /// <summary>
    /// Хранене и храните в него
    /// </summary>
    public class MealGroup : ObservableCollection<Food>
    {
        /// <summary>
        /// Хранене
        /// </summary>
        public Meal Meal { get; set; }

        /// <summary>
        /// Колекция от храни
        /// </summary>
        public ObservableCollection<Food> Foods => this;
    }
}
