namespace MobileFitness.Models
{
    using System;

    public class Weight
    {
        public int Id { get; set; }

        public float Kilograms { get; set; }

        public DateTime Date { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
