using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NutritionTracker
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            var foodItems = new List<FoodItem>()
            {
                new FoodItem{Name="Apple", Calories=90, Proteins=1, Carbs=20, Fats=1},
                new FoodItem{Name="Sandwich", Calories=390, Proteins=10, Carbs=50, Fats=10},
                new FoodItem{Name="Cheese Stick", Calories=120, Proteins=7, Carbs=14, Fats=7},
                new FoodItem{Name="Bacon Slice", Calories=90, Proteins=6, Carbs=20, Fats=15}
            };

            var totalCalories = 0;
            var totalProteins = 0;
            var totalCarbs = 0;
            var totalFats = 0;


            foreach (var foodItem in foodItems)
            {
                totalCalories += foodItem.Calories;
                totalProteins += foodItem.Proteins;
                totalCarbs += foodItem.Carbs;
                totalFats += foodItem.Fats;
            }

            totalCaloriesTextBox.Text = totalCalories.ToString();
            totalProteinsTextBox.Text = totalProteins.ToString();
            totalCarbsTextBox.Text = totalCarbs.ToString();
            totalFatsTextBox.Text = totalFats.ToString();
        }
    }
}