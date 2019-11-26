namespace MobileFitness.Models
{
    using System;
    using System.Collections.Generic;

    using MobileFitness.Models.Enums;

    /// <summary>
    /// Клас, който описва потребител
    /// </summary>
    public class User
    {
        /// <summary>
        /// Създава нов потребител
        /// </summary>
        public User()
        {
            this.Meals = new HashSet<Meal>();
            this.Weights = new HashSet<Weight>();
            this.UsersMacronutrients = new HashSet<UserMacronutrient>();
        }

        /// <summary>
        /// Уникален индентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Потребителско име
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Електронна поща
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Парола
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Салт, използван за хеширане на паролата
        /// </summary>
        public string Salt { get; set; }

        /// <summary>
        /// Дата на раждане
        /// </summary>
        public DateTime Birthdate { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Цел
        /// </summary>
        public Goal Goal { get; set; }

        /// <summary>
        /// Височина в метри
        /// </summary>
        public float HeightInMeters { get; set; }

        /// <summary>
        /// Колекция от всички хранения на потребителя
        /// </summary>
        public ICollection<Meal> Meals { get; set; }

        /// <summary>
        /// Колекция от всички записи на тегло на потребителя
        /// </summary>
        public ICollection<Weight> Weights { get; set; }

        /// <summary>
        /// Колекция от всички записи на макронутриенти на потребителя
        /// </summary>
        public ICollection<UserMacronutrient> UsersMacronutrients { get; set; }
    }
}
