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
            FoodEntities db = new FoodEntities();
            var dbfoodItems = db.FoodItems;
            var newFoodItem = new FoodItem();

            newFoodItem.FoodEntryId = Guid.NewGuid();
            newFoodItem.Name = newNameTextBox.Text;
            newFoodItem.Calories = int.Parse(newCaloriesTextBox.Text);
            newFoodItem.Proteins = int.Parse(newProteinsTextBox.Text);
            newFoodItem.Carbs = int.Parse(newCarbsTextBox.Text);
            newFoodItem.Fats = int.Parse(newFatsTextBox.Text);
            newFoodItem.DateEntered = DateTime.Now;

            dbfoodItems.Add(newFoodItem);
            
           
            //Delete SQL entries older than 7 days
            foreach (var foodItem in dbfoodItems)
            {
                TimeSpan weekTimeFrame = DateTime.Today.Subtract(foodItem.DateEntered);

                if (weekTimeFrame.TotalDays > 7)
                {
                    dbfoodItems.Remove(foodItem);
                }
            }

            //UPDATE DATABASE
            db.SaveChanges();

            //Nutrient Totals
            var totalCalories = 0;
            var totalProteins = 0;
            var totalCarbs = 0;
            var totalFats = 0;

            //Move this to DailyTotalsCalculator
            foreach (var foodItem in dbfoodItems)
            {
                if (foodItem.DateEntered.Date == DateTime.Today)
                {
                    totalCalories += foodItem.Calories;
                    totalProteins += foodItem.Proteins;
                    totalCarbs += foodItem.Carbs;
                    totalFats += foodItem.Fats;
                }
            }

            DisplayDailyTotals(totalCalories, totalProteins, totalCarbs, totalFats);
                                    
            //Bind data to GridView
            foodGridView.DataSource = dbfoodItems.ToList();
            foodGridView.DataBind();
        }

        private void DisplayDailyTotals(int calories, int proteins, int carbs, int fats)
        {
            totalCaloriesTextBox.Text = calories.ToString();
            totalProteinsTextBox.Text = proteins.ToString();
            totalCarbsTextBox.Text = carbs.ToString();
            totalFatsTextBox.Text = fats.ToString();
        }
    }
}