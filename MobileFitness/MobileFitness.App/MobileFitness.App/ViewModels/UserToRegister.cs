namespace MobileFitness.App.ViewModels
{
    using MobileFitness.Models.Enums;
    using System;

    public class UserToRegister
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public DateTime Birthdate { get; set; }

        /// <summary>
        /// Male --> 1
        /// Female --> 2
        /// </summary>
        public int Gender { get; set; }

        public float Weight { get; set; }
    }
}
