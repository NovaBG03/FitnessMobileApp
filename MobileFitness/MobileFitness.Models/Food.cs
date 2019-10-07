namespace MobileFitness.Models
{
    using System;
    using System.Collections.Generic;

    public class Food
    {
        public Food()
        {
            this.MealsFoods = new HashSet<MealFood>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int MacronutrientId { get; set; }

        public Macronutrient Macronutrient { get; set; }

        public ICollection<MealFood> MealsFoods { get; set; }
    }
}
