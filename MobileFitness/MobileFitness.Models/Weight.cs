namespace MobileFitness.Models
{
    using System;

    /// <summary>
    /// Клас, който описва теглото на потребител
    /// </summary>
    public class Weight
    {   
        /// <summary>
        /// Уникален индентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Теглото на потребителя в килограми
        /// </summary>
        public float Kilograms { get; set; }

        /// <summary>
        /// Датата, когато е измерено това тегло
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
    }
}
