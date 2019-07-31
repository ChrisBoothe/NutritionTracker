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
            var dbFoodItems = db.FoodItems;
            var newFoodItem = new FoodItem();
            var dailyTotalsCalculator = new DailyTotalsCalculator();
                       
            newFoodItem.FoodEntryId = Guid.NewGuid();
            newFoodItem.Name = newNameTextBox.Text;
            newFoodItem.Calories = int.Parse(newCaloriesTextBox.Text);
            newFoodItem.Proteins = int.Parse(newProteinsTextBox.Text);
            newFoodItem.Carbs = int.Parse(newCarbsTextBox.Text);
            newFoodItem.Fats = int.Parse(newFatsTextBox.Text);
            newFoodItem.DateEntered = DateTime.Now;

            dbFoodItems.Add(newFoodItem);
            FoodItemManager.RemoveOldEntries(dbFoodItems);
            db.SaveChanges();
                        
            dailyTotalsCalculator.SumMacros(dbFoodItems);
            dailyTotalsCalculator.DetermineRatio(dbFoodItems);
            DisplayDailyTotals(dailyTotalsCalculator);            
            
            foodGridView.DataSource = dbFoodItems.ToList();
            foodGridView.DataBind();
        }

        private void DisplayDailyTotals(DailyTotalsCalculator dailyTotalsCalculator)
        {
            totalCaloriesTextBox.Text = dailyTotalsCalculator.TotalCalories.ToString();
            totalProteinsTextBox.Text = dailyTotalsCalculator.TotalProteins.ToString();
            totalCarbsTextBox.Text = dailyTotalsCalculator.TotalCarbs.ToString();
            totalFatsTextBox.Text = dailyTotalsCalculator.TotalFats.ToString();

            ratioLabel.Text = string.Format("Proteins: {0:0.0}% - Carbs: {1:0.0}% - Fats: {2:0.0}%",
               dailyTotalsCalculator.PercentProteins,
               dailyTotalsCalculator.PercentCarbs,
               dailyTotalsCalculator.PercentFats);
        }
    }
}