namespace MobileFitness.App.ViewModels
{
    using System;

    public class UserToRegister
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public DateTime Birthdate { get; set; }

        /// <summary>
        /// NotSelected = -1
        /// Male = 0,
        /// Female = 1
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// NotSelected = -1
        /// Maintain = 0,
        /// LoseFat = 1,
        /// GainMuscle = 2
        /// </summary>
        public int Goal { get; set; }

        public float WeightInKilograms { get; set; }

        public float HeightInMeters { get; set; }
    }
}
