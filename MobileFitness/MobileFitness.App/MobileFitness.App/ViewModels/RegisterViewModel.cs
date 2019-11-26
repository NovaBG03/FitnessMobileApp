namespace MobileFitness.App.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Net.Mail;
    using System.Text;
    using System.Windows.Input;
    using Microsoft.EntityFrameworkCore;
    using MobileFitness.App.Utils;
    using MobileFitness.App.Views;
    using MobileFitness.Data;
    using MobileFitness.Models;
    using MobileFitness.Models.Enums;
    using Xamarin.Forms;

    /// <summary>
    /// ViewModel за регистрация на потребител
    /// </summary>
    public class RegisterViewModel : BaseViewModel
    {
        private readonly MobileFitnessContext context;

        private string username;
        private string email;
        private string password;
        private string confirmPassword;
        private DateTime birthdate;
        private int genderIndex;
        private int goalIndex;
        private float weightInKilograms;
        private float heightInMeters;

        /// <summary>
        /// Създава нов ViewModel
        /// </summary>
        public RegisterViewModel()
        {
            this.context = DependencyService.Get<MobileFitnessContext>();
            this.RegisterCommand = new Command(this.OnRegister);

            this.Genders = new ObservableCollection<Gender>(Enum.GetValues(typeof(Gender))
                .OfType<Gender>()
                .ToList());

            this.Goals = new ObservableCollection<Goal>(Enum.GetValues(typeof(Goal))
                .OfType<Goal>()
                .ToList());

            this.Birthdate = DateTime.Today;
        }

        /// <summary>
        /// Потребителско име
        /// </summary>
        public string Username
        {
            get => username;
            set
            {
                username = value;
                this.OnPropertyChanged(nameof(this.Username));
            }
        }

        /// <summary>
        /// Електронна поща
        /// </summary>
        public string Email
        {
            get => email;
            set
            {
                email = value?.ToLower();
                this.OnPropertyChanged(nameof(this.Email));
            }
        }

        /// <summary>
        /// Парола
        /// </summary>
        public string Password
        {
            get => password;
            set
            {
                password = value;
                this.OnPropertyChanged(nameof(this.Password));
            }
        }

        /// <summary>
        /// Потвърждение на парола
        /// </summary>
        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                confirmPassword = value;
                this.OnPropertyChanged(nameof(this.ConfirmPassword));
            }
        }

        /// <summary>
        /// Рождена дата
        /// </summary>
        public DateTime Birthdate
        {
            get => birthdate;
            set
            {
                birthdate = value;
                this.OnPropertyChanged(nameof(this.Birthdate));
            }
        }

        /// <summary>
        /// Индекс на пола
        /// </summary>
        public int GenderIndex
        {
            get => genderIndex;
            set
            {
                genderIndex = value;
                this.OnPropertyChanged(nameof(this.GenderIndex));
            }
        }

        /// <summary>
        /// Индекс на целта
        /// </summary>
        public int GoalIndex
        {
            get => goalIndex;
            set
            {
                goalIndex = value;
                this.OnPropertyChanged(nameof(this.GoalIndex));
            }
        }

        /// <summary>
        /// Тегло в килограми
        /// </summary>
        public float WeightInKilograms
        {
            get => weightInKilograms;
            set
            {
                weightInKilograms = (float)Math.Round(value, 2);
                this.OnPropertyChanged(nameof(this.WeightInKilograms));
            }
        }


        /// <summary>
        /// Височина в метри
        /// </summary>
        public float HeightInMeters
        {
            get => heightInMeters;
            set
            {
                heightInMeters = (float)Math.Round(value, 2);
                this.OnPropertyChanged(nameof(this.HeightInMeters));
            }
        }

        /// <summary>
        /// Всияки възможни полове
        /// </summary>
        public ObservableCollection<Gender> Genders { get; private set; }

        /// <summary>
        /// Всияки възможни цели
        /// </summary>
        public ObservableCollection<Goal> Goals { get; private set; }

        /// <summary>
        /// Команда за регистриране
        /// </summary>
        public ICommand RegisterCommand { get; private set; }

        /// <summary>
        /// Създаване на нов профил
        /// </summary>
        private void OnRegister()
        {
            if (string.IsNullOrEmpty(this.Username)
                || string.IsNullOrWhiteSpace(this.Username)
                || this.Username.Length < 4)
            {
                this.DisplayInvalidPrompt("Username must be 4 or more symbols!");
                return;
            }

            if (string.IsNullOrEmpty(this.Email)
                || string.IsNullOrWhiteSpace(this.Email)
                || !IsValidMailAddress(this.Email))
            {
                this.DisplayInvalidPrompt("Please enter correct Email!");
                return;
            }

            if (this.context.Users.Any(u => u.Email == this.Email))
            {
                this.DisplayInvalidPrompt("Account with this Email already exists!");
                return;
            }

            if (this.context.Users.Any(u => u.Username == this.Username))
            {
                this.DisplayInvalidPrompt("Account with this Username already exists!");
                return;
            }

            if (string.IsNullOrEmpty(this.Password)
                || string.IsNullOrWhiteSpace(this.Password)
                || this.Password.Length < 6)
            {
                this.DisplayInvalidPrompt("Password must be 6 or more symbols!");
                return;
            }

            if (this.Password != this.ConfirmPassword)
            {
                this.DisplayInvalidPrompt("Password does not match!");
                return;
            }

            if (this.GoalIndex == -1)
            {
                this.DisplayInvalidPrompt("Please select Goal!");
                return;
            }

            if (this.GenderIndex == -1)
            {
                this.DisplayInvalidPrompt("Please select Gender!");
                return;
            }

            if (this.WeightInKilograms > 300 || this.WeightInKilograms < 20)
            {
                this.DisplayInvalidPrompt("Please enter correct Weight in kilograms!");
                return;
            }

            if (this.HeightInMeters > 2.80 || this.HeightInMeters < 1)
            {
                this.DisplayInvalidPrompt("Please enter correct Height in Meters!");
                return;
            }

            var user = new User()
            {
                Username = this.Username,
                Email = this.Email,
                Salt = Convert.ToBase64String(Common.GetRandomSalt(16)),
                Birthdate = this.Birthdate,
                Gender = (Gender)this.GenderIndex,
                Goal = (Goal)this.GoalIndex,
                HeightInMeters = this.HeightInMeters
            };
            user.Weights.Add(new Weight()
            {
                Date = DateTime.Today,
                Kilograms = this.WeightInKilograms
            });

            MacronutrientManager.SetNewMacronutrientGoal(user);

            user.Password = Convert.ToBase64String(
                 Common.SaltHashPassword(
                    Encoding.ASCII.GetBytes(this.Password),
                    Convert.FromBase64String(user.Salt)));

            try
            {
                this.context.Users.Add(user);
                this.context.SaveChanges();
                this.DisplayInvalidPrompt("Registered successfully!");
            }
            catch (Exception)
            {
                this.DisplayInvalidPrompt("Something whent wrong.");
            }
        }

        /// <summary>
        /// Проверява дали дадена електронна поща е валидна
        /// </summary>
        /// <param name="emailaddress">Електронна поща</param>
        /// <returns></returns>
        private static bool IsValidMailAddress(string emailaddress)
        {
            try
            {
                MailAddress mail = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
