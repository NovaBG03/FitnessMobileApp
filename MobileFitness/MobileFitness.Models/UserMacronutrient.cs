namespace MobileFitness.Models
{
    using System;

    public class UserMacronutrient
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int MacronutrientId { get; set; }

        public Macronutrient Macronutrient { get; set; }

        public DateTime Date { get; set; }
    }
}
