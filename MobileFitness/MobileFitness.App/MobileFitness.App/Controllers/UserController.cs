namespace MobileFitness.App.Controllers
{
    using Newtonsoft.Json;
    using System;
    using System.Text;
    using System.Linq;
    using System.Net.Mail;
    using System.Threading.Tasks;

    using MobileFitness.App.Utils;
    using MobileFitness.App.ViewModels;
    using MobileFitness.Data;
    using MobileFitness.Models;
    using MobileFitness.App.Controllers.Contracts;
    using MobileFitness.Models.Enums;

    using Xamarin.Forms;

    public class UserController : IUserController
    {
        private readonly MobileFitnessContext context;

        /// <summary>
        /// Creates new UserController
        /// </summary>
        /// <param name="context"></param>
        public UserController()
        {
            this.context = DependencyService.Get<MobileFitnessContext>();
        }

        /// <summary>
        /// Tries to login user with email and password
        /// </summary>
        /// <param name="userToregister"></param>
        /// <returns>Message</returns>
        public async Task<object> Login(UserToRegister userToregister)
        {
            var email = userToregister.Email?.ToLower();
            var password = userToregister.Password;

            var user = this.context.Users
                    .Where(u => u.Email == email)
                    .FirstOrDefault();

            if (user == null)
            {
                return JsonConvert.SerializeObject("Wrong Email or Password!");
            }

            var hashedPassword = Convert.ToBase64String(
                await Common.SaltHashPassword(
                    Encoding.ASCII.GetBytes(password),
                    Convert.FromBase64String(user.Salt)));

            if (hashedPassword != user.Password)
            {
                return JsonConvert.SerializeObject("Wrong Email or Password!");
            }

            return user;
        }

        /// <summary>
        /// Registers new user
        /// </summary>
        /// <param name="userToRegister"></param>
        /// <returns>Message</returns>
        public async Task<string> Register(UserToRegister userToRegister)
        {
            if (string.IsNullOrEmpty(userToRegister.Username)
                || string.IsNullOrWhiteSpace(userToRegister.Username)
                || userToRegister.Username.Length < 4)
            {
                return JsonConvert.SerializeObject("Username must be 4 or more symbols!");
            }

            if (string.IsNullOrEmpty(userToRegister.Email)
                || string.IsNullOrWhiteSpace(userToRegister.Email)
                || !IsValidMailAddress(userToRegister.Email))
            {
                return JsonConvert.SerializeObject("Please enter correct Email!");
            }

            var emailToLower = userToRegister.Email.ToLower();
            if (this.context.Users.Any(u => u.Email == emailToLower))
            {
                return JsonConvert.SerializeObject("Account with this Email already exists!");
            }

            if (this.context.Users.Any(u => u.Username == userToRegister.Username))
            {
                return JsonConvert.SerializeObject("Account with this Username already exists!");
            }

            if (string.IsNullOrEmpty(userToRegister.Password)
                || string.IsNullOrWhiteSpace(userToRegister.Password)
                || userToRegister.Password.Length < 6)
            {
                return JsonConvert.SerializeObject("Password must be 6 or more symbols!");
            }

            if (userToRegister.Password != userToRegister.ConfirmPassword)
            {
                return JsonConvert.SerializeObject("Password does not match!");
            }

            if (userToRegister.Goal == -1)
            {
                return JsonConvert.SerializeObject("Please select Goal!");
            }

            if (userToRegister.Gender == -1)
            {
                return JsonConvert.SerializeObject("Please select Gender!");
            }

            var weightInKilograms = (float)Math.Round(userToRegister.WeightInKilograms, 2);

            if (weightInKilograms > 300 || weightInKilograms < 20)
            {
                return JsonConvert.SerializeObject("Please enter correct Weight in kilograms!");
            }

            var heightInMeters = (float)Math.Round(userToRegister.HeightInMeters, 2);

            if (heightInMeters > 2.80 || heightInMeters < 1)
            {
                return JsonConvert.SerializeObject("Please enter correct Height in Meters!");
            }

            var user = new User()
            {
                Username = userToRegister.Username,
                Email = emailToLower,
                Salt = Convert.ToBase64String(await Common.GetRandomSalt(16)),
                Birthdate = userToRegister.Birthdate,
                Gender = (Gender)userToRegister.Gender,
                Goal = (Goal)userToRegister.Goal,
                HeightInMeters = heightInMeters
            };
            user.Weights.Add(new Weight()
            {
                Date = DateTime.Now,
                Kilograms = weightInKilograms
            });

            this.SetMacronutrientGoal(user);

            user.Password = Convert.ToBase64String(
                await Common.SaltHashPassword(
                    Encoding.ASCII.GetBytes(userToRegister.Password),
                    Convert.FromBase64String(user.Salt)));

            try
            {
                await this.context.Users.AddAsync(user);
                await this.context.SaveChangesAsync();
                return JsonConvert.SerializeObject("Registered successfully!");
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject("Something whent wrong.");
            }
        }

        private void SetMacronutrientGoal(User user)
        {
            var bmr = CalculateBmr(user);
            var calories = CalculateCaloriesByGoal(user, bmr * 1.35);
            var caloriesLeft = calories;

            var protein = GetCurrentWeight(user) * 1.8;
            var proteinCalories = protein * 4;

            caloriesLeft -= proteinCalories;

            var fat = GetCurrentWeight(user);
            var fatCalories = fat * 9;

            if (fatCalories / calories > 0.4)
            {
                fatCalories = calories * 0.4;
                fat = fatCalories / 9;
            }

            caloriesLeft -= fatCalories;

            var carbohydrate = caloriesLeft / 4;

            user.Macronutrient = new Macronutrient()
            {
                Protein = (float)Math.Floor(protein),
                Fat = (float)Math.Floor(fat),
                Carbohydrate = (float)Math.Floor(carbohydrate)
            };
        }

        private double CalculateCaloriesByGoal(User user, double calories)
        {
            if (user.Goal == Goal.Maintain)
            {
                return calories;
            }

            double caloricDiff = 0.1 * calories;
            if (caloricDiff > 250)
            {
                caloricDiff = 250;
            }

            if (user.Goal == Goal.LoseFat)
            {
                return calories - caloricDiff;

            }

            //if user.Goal == Goal.Maintain
            return calories + caloricDiff;
        }

        private double CalculateBmr(User user)
        {
            double weightInKilograms = GetCurrentWeight(user);

            double heightInCentimetre = user.HeightInMeters * 100;

            var ageInYears = (int)((DateTime.Now - user.Birthdate).Days / 365.2425);

            if (user.Gender == Gender.Male)
            {
                return 66.47 + (13.75 * weightInKilograms) + (5.003 * heightInCentimetre) - (6.755 * ageInYears);
            }

            //if user.Gender == Gender.Female
            return 655.1 + (9.563 * weightInKilograms) + (1.85 * heightInCentimetre) - (4.676 * ageInYears);
        }

        private static double GetCurrentWeight(User user)
        {
            return user.Weights
                .OrderByDescending(w => w.Date)
                .First()
                .Kilograms;
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
