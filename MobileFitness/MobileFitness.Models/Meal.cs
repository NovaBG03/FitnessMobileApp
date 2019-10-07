namespace MobileFitness.Models
{
    using System;
    using System.Collections.Generic;

    public class Meal
    {
        public Meal()
        {
            this.MealsFoods = new HashSet<MealFood>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public ICollection<MealFood> MealsFoods { get; set; }
    }
}
