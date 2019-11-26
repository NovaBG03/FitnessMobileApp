namespace MobileFitness.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Клас, който описва макронутриенти
    /// </summary>
    public class Macronutrient
    {
        /// <summary>
        /// Създава нови макронутриенти
        /// </summary>
        public Macronutrient()
        {
            this.Foods = new HashSet<Food>();
            this.UsersMacronutrients = new HashSet<UserMacronutrient>();
        }

        /// <summary>
        /// Уникален индентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Количесто белтъци в грамове
        /// </summary>
        public float Protein { get; set; }

        /// <summary>
        /// Количесто мазнини в грамове
        /// </summary>
        public float Fat { get; set; }

        /// <summary>
        /// Количесто въглехидрати в грамове
        /// </summary>
        public float Carbohydrate { get; set; }

        /// <summary>
        /// Колекция от храни с такива макронутриенти
        /// </summary>
        public ICollection<Food> Foods { get; set; }

        /// <summary>
        /// Колекция от потребители с цел тези макронутриенти
        /// </summary>
        public ICollection<UserMacronutrient> UsersMacronutrients { get; set; }
    }
}

