namespace MobileFitness.Models
{
    using System;
    using System.Collections.Generic;

    using MobileFitness.Models.Enums;

    public class User
    {
        public User()
        {
            this.Meals = new HashSet<Meal>();
            this.Weights = new HashSet<Weight>();
            this.UsersMacronutrients = new HashSet<UserMacronutrient>();
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public DateTime Birthdate { get; set; }

        public Gender Gender { get; set; }

        public Goal Goal { get; set; }

        public float HeightInMeters { get; set; }

        public ICollection<Meal> Meals { get; set; }

        public ICollection<Weight> Weights { get; set; }

        public ICollection<UserMacronutrient> UsersMacronutrients { get; set; }
    }
}
