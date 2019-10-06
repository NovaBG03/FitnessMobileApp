namespace MobileFitness.App.Controllers
{
    using Newtonsoft.Json;
    using System;
    using System.Text;
    using System.Linq;
    using System.Threading.Tasks;

    using MobileFitness.App.Utils;
    using MobileFitness.App.ViewModels;
    using MobileFitness.Data;
    using MobileFitness.Models;
    using MobileFitness.App.Controllers.Contracts;

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
        /// <param name="userDto"></param>
        /// <returns>Message</returns>
        public async Task<string> Login(UserDto userDto)
        {
            var email = userDto.Email;
            var password = userDto.Password;

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
        /// <param name="userDto"></param>
        /// <returns>Message</returns>
        public async Task<string> Register(UserDto userDto)
        {
            if (this.context.Users.Any(u => u.Email == userDto.Email))
            {
                return JsonConvert.SerializeObject("Account with this Email already exists!");
            }

            if (this.context.Users.Any(u => u.Username == userDto.Username))
            {
                return JsonConvert.SerializeObject("Account with this Username already exists!");
            }

            var user = new User()
            {
                Username = userDto.Username,
                Email = userDto.Email,
                Salt = Convert.ToBase64String(await Common.GetRandomSalt(16)),
            };
            user.Password = Convert.ToBase64String(
                await Common.SaltHashPassword(
                    Encoding.ASCII.GetBytes(userDto.Password),
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
    }
}
