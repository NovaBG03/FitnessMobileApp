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
        public async Task<string> Login(UserToRegister userToregister)
        {
            var email = userToregister.Email;
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

            return JsonConvert.SerializeObject(user);
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

            if (string.IsNullOrEmpty(userToRegister.Email))
            {
                return JsonConvert.SerializeObject("Please enter correct Email!");
            }

            if (this.context.Users.Any(u => u.Email == userToRegister.Email))
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
                Email = userToRegister.Email.ToLower(),
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

            return JsonConvert.SerializeObject(user);

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
            switch (user.Goal)
            {
                case Goal.Maintain:
                    user.Macronutrient = this.GetMacronutrientGoalToMaintain(user);
                    break;
                case Goal.LoseFat:
                    user.Macronutrient = this.GetMacronutrientGoalToLoseFat(user);
                    break;
                case Goal.GainMuscle:
                    user.Macronutrient = this.GetMacronutrientGoalToGainMuscle(user);
                    break;
            }
        }

        private Macronutrient GetMacronutrientGoalToGainMuscle(User user)
        {
            throw new NotImplementedException();
        }

        private Macronutrient GetMacronutrientGoalToLoseFat(User user)
        {
            throw new NotImplementedException();
        }

        private Macronutrient GetMacronutrientGoalToMaintain(User user)
        {
            throw new NotImplementedException();
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
