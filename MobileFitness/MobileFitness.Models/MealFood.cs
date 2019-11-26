namespace MobileFitness.Models
{
    using System;

    /// <summary>
    /// Клас, който описва много към много връзка на хранения и храни
    /// </summary>
    public class MealFood
    {
        /// <summary>
        /// Уникален индентификатор на хранене
        /// </summary>
        public int MealId { get; set; }

        /// <summary>
        /// Хранене
        /// </summary>
        public Meal Meal { get; set; }

        /// <summary>
        /// Уникален индентификатор на храна
        /// </summary>
        public int FoodId { get; set; }

        /// <summary>
        /// Храна
        /// </summary>
        public Food Food { get; set; }

        /// <summary>
        /// Количество храна в грамове
        /// </summary>
        public float FoodQuantity { get; set; }
    }
}
