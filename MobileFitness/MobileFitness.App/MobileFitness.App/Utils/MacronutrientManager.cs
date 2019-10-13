namespace MobileFitness.App.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using MobileFitness.Models;
    using MobileFitness.Models.Enums;

    public static class MacronutrientManager
    {
        public static void SetNewMacronutrientGoal(User user)
        {
            var bmr = CalculateBmr(user);
            var calories = CalculateCaloriesByGoal(user, bmr * 1.35);
            var caloriesLeft = calories;

            var protein = GetCurrentWeight(user) * 1.8;
            var proteinCalories = protein * 4;

            caloriesLeft -= proteinCalories;

            var fat = GetCurrentWeight(user);
            var fatCalories = fat * 9;

            if (fatCalories / calories > 0.4)
            {
                fatCalories = calories * 0.4;
                fat = fatCalories / 9;
            }

            caloriesLeft -= fatCalories;

            var carbohydrate = caloriesLeft / 4;

            user.UsersMacronutrients.Add(new UserMacronutrient()
            {
                Macronutrient = new Macronutrient()
                {
                    Protein = (float)Math.Floor(protein),
                    Fat = (float)Math.Floor(fat),
                    Carbohydrate = (float)Math.Floor(carbohydrate)
                },
                Date = DateTime.Today
            });
        }

        private static double CalculateCaloriesByGoal(User user, double calories)
        {
            if (user.Goal == Goal.Maintain)
            {
                return calories;
            }

            double caloricDiff = 0.1 * calories;
            if (caloricDiff > 250)
            {
                caloricDiff = 250;
            }

            if (user.Goal == Goal.LoseFat)
            {
                return calories - caloricDiff;

            }

            //if user.Goal == Goal.Maintain
            return calories + caloricDiff;
        }

        private static double CalculateBmr(User user)
        {
            double weightInKilograms = GetCurrentWeight(user);

            double heightInCentimetre = user.HeightInMeters * 100;

            var ageInYears = Common.GetAgeDiff(DateTime.Today, user.Birthdate);

            if (user.Gender == Gender.Male)
            {
                return 66.47 + (13.75 * weightInKilograms) + (5.003 * heightInCentimetre) - (6.755 * ageInYears);
            }

            //if user.Gender == Gender.Female
            return 655.1 + (9.563 * weightInKilograms) + (1.85 * heightInCentimetre) - (4.676 * ageInYears);
        }

        private static double GetCurrentWeight(User user)
        {
            return user.Weights
                .OrderByDescending(w => w.Date)
                .First()
                .Kilograms;
        }
    }
}
