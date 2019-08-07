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
            errorLabel.Text = "";            
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            FoodEntities db = new FoodEntities();
            var dbFoodItems = db.FoodItems;
            var newFoodItem = new FoodItem();
            var dailyTotalsCalculator = new DailyTotalsCalculator();

            int newCalories;
            int newProteins;
            int newCarbs;
            int newFats;

            if (!int.TryParse(newCaloriesTextBox.Text, out newCalories) ||
                !int.TryParse(newProteinsTextBox.Text, out newProteins) ||
                !int.TryParse(newCarbsTextBox.Text, out newCarbs) ||
                !int.TryParse(newFatsTextBox.Text, out newFats))
            {
                errorLabel.Text = "You must input a valid number!";
                return;
            }

            AssignUserInput(newFoodItem, newCalories, newProteins, newCarbs, newFats);
            dbFoodItems.Add(newFoodItem);
            FoodItemManager.RemoveOldEntries(dbFoodItems);
            db.SaveChanges();
                        
            dailyTotalsCalculator.SumMacros(dbFoodItems);
            dailyTotalsCalculator.DetermineRatio(dbFoodItems);

            DisplayDailyTotals(dailyTotalsCalculator);
            DisplayMacroRatio(dailyTotalsCalculator);
                       
            foodGridView.DataSource = dbFoodItems/*.Where(p => p.DateEntered.Date == DateTime.Today)*/.OrderBy(p => p.DateEntered).ToList();                        
            foodGridView.DataBind();
        }

        private void DisplayDailyTotals(DailyTotalsCalculator dailyTotalsCalculator)
        {
            totalCaloriesLabel.Text = dailyTotalsCalculator.TotalCalories.ToString();
            totalProteinsLabel.Text = dailyTotalsCalculator.TotalProteins.ToString();
            totalCarbsLabel.Text = dailyTotalsCalculator.TotalCarbs.ToString();
            totalFatsLabel.Text = dailyTotalsCalculator.TotalFats.ToString();
        }

        private void DisplayMacroRatio(DailyTotalsCalculator dailyTotalsCalculator)
        {
            ratioLabel.Text = string.Format("Proteins: {0:0.0}% <br/> Carbs: {1:0.0}% <br/> Fats: {2:0.0}%",
               dailyTotalsCalculator.PercentProteins,
               dailyTotalsCalculator.PercentCarbs,
               dailyTotalsCalculator.PercentFats);
        }

        private void AssignUserInput(FoodItem newFoodItem, int calories, int proteins, int carbs, int fats)
        {                      
            newFoodItem.FoodEntryId = Guid.NewGuid();
            newFoodItem.Name = newNameTextBox.Text;
            newFoodItem.Calories = calories;
            newFoodItem.Proteins = proteins;
            newFoodItem.Carbs = carbs;
            newFoodItem.Fats = fats;
            newFoodItem.DateEntered = DateTime.Now;
        }
    }
}