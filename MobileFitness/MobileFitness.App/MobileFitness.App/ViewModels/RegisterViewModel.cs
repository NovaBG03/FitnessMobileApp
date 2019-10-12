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

    public class RegisterViewModel : INotifyPropertyChanged
    {
        public Action<string> DisplayInvalidPrompt;
        public event PropertyChangedEventHandler PropertyChanged;

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

        public RegisterViewModel()
        {
            this.context = DependencyService.Get<MobileFitnessContext>();
            this.RegisterCommand = new Command(OnRegister);

            this.Genders = new ObservableCollection<Gender>(Enum.GetValues(typeof(Gender))
                .OfType<Gender>()
                .ToList());

            this.Goals = new ObservableCollection<Goal>(Enum.GetValues(typeof(Goal))
                .OfType<Goal>()
                .ToList());

            this.Birthdate = DateTime.Today;
        }

        public string Username
        {
            get => username;
            set
            {
                username = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Username)));
            }
        }

        public string Email
        {
            get => email;
            set
            {
                email = value?.ToLower();
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Email)));
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Password)));
            }
        }

        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                confirmPassword = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.ConfirmPassword)));
            }
        }

        public DateTime Birthdate
        {
            get => birthdate;
            set
            {
                birthdate = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Birthdate)));
            }
        }

        public int GenderIndex
        {
            get => genderIndex;
            set
            {
                genderIndex = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.GenderIndex)));
            }
        }

        public int GoalIndex
        {
            get => goalIndex;
            set
            {
                goalIndex = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.GoalIndex)));
            }
        }

        public float WeightInKilograms
        {
            get => weightInKilograms;
            set
            {
                weightInKilograms = (float)Math.Round(value, 2);
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.WeightInKilograms)));
            }
        }

        public float HeightInMeters
        {
            get => heightInMeters;
            set
            {
                heightInMeters = (float)Math.Round(value, 2);
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.HeightInMeters)));
            }
        }

        public ObservableCollection<Gender> Genders { get; private set; }

        public ObservableCollection<Goal> Goals { get; private set; }

        public ICommand RegisterCommand { get; private set; }

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
                Date = DateTime.Now,
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
