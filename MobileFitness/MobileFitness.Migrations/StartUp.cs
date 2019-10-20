namespace MobileFitness.Migrations
{
    using System;
    using System.Collections.Generic;
    using MobileFitness.Data;
    using MobileFitness.Models;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            //assembly for migrations

            using (var context = new MobileFitnessContext())
            {
                //SeedData(context);
            }
        }

        private static void SeedData(MobileFitnessContext context)
        {
            List<CustomFood> foods = new List<CustomFood>()
            {
                //crabs fat protein
                new CustomFood("Curds", 2F, 7F, 18F),
                new CustomFood("Cheese", 2F, 30F, 20F),
                new CustomFood("Yoghurt 0.5%", 4F, 0.5F, 3F),
                new CustomFood("Yoghurt 2%", 3F, 2F, 3F),
                new CustomFood("Yoghurt 3.6%", 2.5F, 3.6F, 3.2F),
                new CustomFood("Yogurt 6.5%", 2.5F, 6.5F, 3.5F),
                new CustomFood("Mascarpone", 2F, 41F, 5F),
                new CustomFood("Mozzarella", 2F, 22F, 22F),
                new CustomFood("Parmesan", 0F, 33F, 40F),
                new CustomFood("Milk 0.1%", 5F, 0.5F, 3F),
                new CustomFood("Milk 0.1%", 5F, 1.5F, 3F),
                new CustomFood("Milk 0.1%", 5F, 3F, 3F),
                new CustomFood("Egg", 1.1F, 11F, 13F),
                new CustomFood("Bacon", 0F, 40F, 14F),
                new CustomFood("Lukanka", 0F, 40F, 25F),
                new CustomFood("Chicken Livers", 1F, 5F, 17F),
                new CustomFood("Chicken Hearts", 1F, 9F, 15F),
                new CustomFood("Chicken Breasts", 0F, 1F, 23F),
                new CustomFood("Pork Mince", 0F, 21F, 17F),
                new CustomFood("Pork Livers", 2F, 4F, 21F),
                new CustomFood("Veal Mince", 0F, 30F, 14F),
                new CustomFood("Beef Liver", 4F, 4F, 20F),
                new CustomFood("Beef Steak", 0F, 4F, 22F),
                new CustomFood("Beef Fillet", 0F, 3F, 21F),
                new CustomFood("Ham", 0F, 5F, 18F),
                new CustomFood("Avocado", 2F, 20F, 2F),
                new CustomFood("Pineapple", 11F, 0.2F, 0.4F),
                new CustomFood("Asparagus", 2F, 0.6F, 3F),
                new CustomFood("Banana", 23F, 0.3F, 1.2F),
                new CustomFood("Broccoli", 1.8F, 1F, 4.4F),
                new CustomFood("Brussels Sprouts", 4F, 0F, 5F),
                new CustomFood("Peas", 6F, 10F, 1F),
                new CustomFood("Grapefruit", 5F, 0F, 0.5F),
                new CustomFood("Mushrooms", 0.4F, 0.5F, 1.8F),
                new CustomFood("Watermelon", 7F, 0.3F, 0.5F),
                new CustomFood("Tomato", 3F, 0F, 0.7F),
                new CustomFood("Cabbage", 4F, 0.4F, 1.7F),
                new CustomFood("Green Beans", 7F, 0F, 2F),
                new CustomFood("Kiwi", 11F, 0F, 1F),
                new CustomFood("Cucumber", 1.5F, 0F, 0.7F),
                new CustomFood("Pears", 10F, 0F, 0.3F),
                new CustomFood("Blackberries", 14F, 0F, 0.7F),
                new CustomFood("Lemons", 3F, 0F, 1F),
                new CustomFood("Fresh Onions", 3F, 0.5F, 2F),
                new CustomFood("Onion", 8F, 0.2F, 1.2F),
                new CustomFood("Raspberries", 5F, 0F, 1.4F),
                new CustomFood("Mango", 14F, 0F, 1F),
                new CustomFood("Mandarins", 8F, 0F, 1F),
                new CustomFood("Lettuce", 1.7F, 0.5F, 0.8F),
                new CustomFood("Carrots", 8F, 0.4F, 0.6F),
                new CustomFood("Nectarines", 9F, 0F, 1F),
                new CustomFood("Oranges", 8F, 0F, 1F),
                new CustomFood("Peaches", 8F, 0F, 1F),
                new CustomFood("Fresh Potatoes", 17F, 0F, 1.5F),
                new CustomFood("Melon", 6F, 0F, 1F),
                new CustomFood("Sweet Potatoes", 20F, 0F, 1F),
                new CustomFood("Spinach", 4F, 0F, 3F),
                new CustomFood("Old Potatoes", 32F, 0F, 4F),
                new CustomFood("Raisins", 70F, 0.4F, 2F),
                new CustomFood("Cherries", 12F, 0.4F, 1F),
                new CustomFood("Peppers", 5F, 0F, 1F),
                new CustomFood("Apples", 12F, 0.1F, 0.4F),
                new CustomFood("Berries", 6F, 0F, 1F),
                new CustomFood("Almonds", 20F, 51F, 21F),
                new CustomFood("Porridge", 30F, 44F, 18F),
                new CustomFood("Hazelnuts", 18F, 62F, 15F),
                new CustomFood("Walnuts", 14F, 65F, 15F),
                new CustomFood("Peanuts", 16F, 49F, 26F),
                new CustomFood("Pistachios", 28F, 44F, 21F),
                new CustomFood("Peanut Butter", 20F, 50F, 25F),
                new CustomFood("Olive Oil", 0F, 100F, 0F),
                new CustomFood("Butter", 0F, 81F, 1F),
                new CustomFood("Olives", 6F, 11F, 1F),
                new CustomFood("Sesame", 23F, 50F, 18F),
                new CustomFood("Ripe Beans", 20F, 1F, 7F),
                new CustomFood("Brown Rice", 76F, 3F, 8F),
                new CustomFood("Lentils", 17F, 0.7F, 8.8F),
                new CustomFood("Rice", 28F, 0.3F, 2.7F)
            };

            for (int i = 1; i <= foods.Count; i++)
            {
                var food = foods[i - 1];

                context.Foods.Add(new Food()
                {
                    Name = food.FoodName,
                    Macronutrient = new Macronutrient()
                    {
                        Carbohydrate = food.Carbs,
                        Fat = food.Fat,
                        Protein = food.Protein,
                    }
                });
            }

            context.SaveChanges();
        }

        private class CustomFood
        {
            public CustomFood(string foodName, float carbs, float fat, float protein)
            {
                FoodName = foodName;
                Carbs = carbs;
                Fat = fat;
                Protein = protein;
            }

            public string FoodName { get; set; }

            public float Carbs { get; set; }

            public float Fat { get; set; }

            public float Protein { get; set; }
        }
    }
}
