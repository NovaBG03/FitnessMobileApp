namespace MobileFitness.Models
{
    using System;
    using System.Collections.Generic;

    public class Macronutrient
    {
        public Macronutrient()
        {
            this.Foods = new HashSet<Food>();
            this.UsersMacronutrients = new HashSet<UserMacronutrient>();
        }

        public int Id { get; set; }

        public float Protein { get; set; }

        public float Fat { get; set; }

        public float Carbohydrate { get; set; }

        public ICollection<Food> Foods { get; set; }

        public ICollection<UserMacronutrient> UsersMacronutrients { get; set; }
    }
}

