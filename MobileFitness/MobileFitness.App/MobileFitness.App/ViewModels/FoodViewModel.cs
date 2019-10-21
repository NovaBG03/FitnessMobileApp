using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using MobileFitness.App.Views;
using MobileFitness.Models;
using Xamarin.Forms;

namespace MobileFitness.App.ViewModels
{
    public class FoodViewModel : BaseViewModel
    {
        private Food food;
        private string foodName;
        private float carbohydrate;
        private float fat;
        private float protein;
        private float foodQuantity;

        public FoodViewModel()
        {
            //this.SaveFood = new Command(this.OnSaveFood);
        }

        public string FoodName
        {
            get => foodName;
            set
            {
                foodName = value;
                this.OnPropertyChanged(nameof(this.FoodName));
            }
        }

        public float Carbohydrate
        {
            get => carbohydrate;
            set
            {
                carbohydrate = value;
                this.OnPropertyChanged(nameof(this.Carbohydrate));
                this.OnPropertyChanged(nameof(this.Calories));
            }
        }

        public float Fat
        {
            get => fat;
            set
            {
                fat = value;
                this.OnPropertyChanged(nameof(this.Fat));
                this.OnPropertyChanged(nameof(this.Calories));
            }
        }

        public float Protein
        {
            get => protein;
            set
            {
                protein = value;
                this.OnPropertyChanged(nameof(this.Protein));
                this.OnPropertyChanged(nameof(this.Calories));
            }
        }

        public float Calories
            => (this.Protein * 4) + (this.Carbohydrate * 4) + (this.Fat * 9);


        public float FoodQuantity
        {
            get => foodQuantity;
            set
            {
                foodQuantity = value;
                this.OnPropertyChanged(nameof(this.FoodQuantity));
                this.UpdateFoodInfo();
            }
        }

        //public ICommand SaveFood { get; set; }

        private void UpdateFoodInfo()
        {
            if (this.food == null)
            {
                return;
            }

            this.Carbohydrate = this.food.Macronutrient.Carbohydrate * this.FoodQuantity / 100;
            this.Fat = this.food.Macronutrient.Fat * this.FoodQuantity / 100;
            this.Protein = this.food.Macronutrient.Protein * this.FoodQuantity / 100;
        }

        public void ShowFoodInfo(object[] args)
        {
            this.food = (Food)args[0];
            this.FoodQuantity = (float)args[1];
            this.FoodName = this.food.Name;

            this.UpdateFoodInfo();
        }

        //private void OnSaveFood()
        //{
        //    if (this.FoodQuantity < 1)
        //    {
        //        this.DisplayInvalidPrompt("Please enter correct Quantity.");
        //    }

        //    MessagingCenter.Send(this, "UpdateFood", new object[] { this.food, this.FoodQuantity });
        //    App.Current.MainPage.Navigation.PopAsync();
        //}
    }
}
