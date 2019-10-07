namespace MobileFitness.Models
{
    using System;
    using System.Collections.Generic;

    public class User
    {
        public User()
        {
            this.Meals = new HashSet<Meal>();
            this.Weights = new HashSet<Weight>();
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public int MacronutrientId { get; set; }

        public Macronutrient Macronutrient { get; set; }

        public ICollection<Meal> Meals { get; set; }

        public ICollection<Weight> Weights { get; set; }
    }
}
