﻿namespace MobileFitness.App.Controllers.Contracts
{
    using MobileFitness.App.ViewModels;
    using System.Threading.Tasks;

    public interface IUserController
    {
        /// <summary>
        /// Registers new user
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns>Message</returns>
        Task<string> Register(UserToRegister userDto);


        /// <summary>
        /// Tries to login user with email and password
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns>Message</returns>
        Task<object> Login(UserToRegister userDto);
    }
}
