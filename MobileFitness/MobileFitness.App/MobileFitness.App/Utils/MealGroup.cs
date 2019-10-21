namespace MobileFitness.App.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Text;
    using MobileFitness.Models;

    public class MealGroup : ObservableCollection<Food>
    {
        public Meal Meal { get; set; }

        public ObservableCollection<Food> Foods => this;
    }
}
