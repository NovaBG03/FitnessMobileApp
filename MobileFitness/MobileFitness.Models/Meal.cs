namespace MobileFitness.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Клас, който описва хранене
    /// </summary>
    public class Meal
    {
        /// <summary>
        /// Създава ново хранене
        /// </summary>
        public Meal()
        {
            this.MealsFoods = new HashSet<MealFood>();
        }

        /// <summary>
        /// Уникален индентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Име на храненето
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата на храненето
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Уникален индентификатор на протребителя
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Потребителя
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Колекция от всички храни за това хранене
        /// </summary>
        public ICollection<MealFood> MealsFoods { get; set; }
    }
}
