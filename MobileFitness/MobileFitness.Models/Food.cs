/// <summary>
/// Моделите за базата данни
/// </summary>
namespace MobileFitness.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Клас, който описва храна
    /// </summary>
    public class Food
    {
        /// <summary>
        /// Създава нова храна
        /// </summary>
        public Food()
        {
            this.MealsFoods = new HashSet<MealFood>();
        }

        /// <summary>
        /// Уникален индентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Има на храната
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Уникален индентификатор на макронутриенти
        /// </summary>
        public int MacronutrientId { get; set; }

        /// <summary>
        /// Макронутриенти на храната
        /// </summary>
        public Macronutrient Macronutrient { get; set; }

        /// <summary>
        /// Храненя, в които участва храната
        /// </summary>
        public ICollection<MealFood> MealsFoods { get; set; }
    }
}
