namespace MobileFitness.Models
{
    using System;

    /// <summary>
    /// Клас, който описва много към много връзка на потребители и макронутриенти
    /// </summary>
    public class UserMacronutrient
    {
        /// <summary>
        /// Уникален индентификатор на протребителя
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Потребителя
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Уникален индентификатор на макронутриент
        /// </summary>
        public int MacronutrientId { get; set; }

        /// <summary>
        /// Макронутриент
        /// </summary>
        public Macronutrient Macronutrient { get; set; }

        /// <summary>
        /// Датата, когато са зададени макронутриенти на потребител
        /// </summary>
        public DateTime Date { get; set; }
    }
}
