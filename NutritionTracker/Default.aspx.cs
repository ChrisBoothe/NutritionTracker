using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

namespace NutritionTracker
{
    public partial class Default : System.Web.UI.Page
    { 
        protected void Page_Load(object sender, EventArgs e)
        {   
            if (!Page.IsPostBack)
            {
                FoodEntities db = new FoodEntities();
                var dbFoodItems = db.FoodItems;
                var dailyTotalsCalculator = new DailyTotalsCalculator();

                dailyTotalsCalculator.SumMacros(dbFoodItems);
                dailyTotalsCalculator.DetermineRatio(dbFoodItems);

                DisplayData(dailyTotalsCalculator, dbFoodItems);
            }

            errorLabel.Text = "";            
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            FoodEntities db = new FoodEntities();
            var dbFoodItems = db.FoodItems;
            var newFoodItem = new FoodItem();
            var dailyTotalsCalculator = new DailyTotalsCalculator();

            if (!ObtainUserInput(newFoodItem))
            {
                errorLabel.Text = "You must input a valid number!";
                return;
            }

            dbFoodItems.Add(newFoodItem);
            FoodItemManager.RemoveOldEntries(dbFoodItems);
            db.SaveChanges();
                        
            dailyTotalsCalculator.SumMacros(dbFoodItems);
            dailyTotalsCalculator.DetermineRatio(dbFoodItems);

            DisplayData(dailyTotalsCalculator, dbFoodItems);
        }

        private bool ObtainUserInput(FoodItem newFoodItem)
        {
            int newCalories;
            int newProteins;
            int newCarbs;
            int newFats;

            if (!int.TryParse(newCaloriesTextBox.Text, out newCalories) ||
                !int.TryParse(newProteinsTextBox.Text, out newProteins) ||
                !int.TryParse(newCarbsTextBox.Text, out newCarbs) ||
                !int.TryParse(newFatsTextBox.Text, out newFats))
            {                
                return false;
            }

            else
            {
                AssignUserInput(newFoodItem, newCalories, newProteins, newCarbs, newFats);
                return true;
            }            
        }

        private void AssignUserInput(FoodItem newFoodItem, int newCalories, int newProteins, int newCarbs, int newFats)
        {
            newFoodItem.FoodEntryId = Guid.NewGuid();
            newFoodItem.Name = newNameTextBox.Text;
            newFoodItem.Calories = newCalories;
            newFoodItem.Proteins = newProteins;
            newFoodItem.Carbs = newCarbs;
            newFoodItem.Fats = newFats;
            newFoodItem.DateEntered = DateTime.Now;            
        }

        private void DisplayData(DailyTotalsCalculator dailyTotalsCalculator, DbSet<FoodItem> dbFoodItems)
        {
            DisplayDailyTotals(dailyTotalsCalculator);
            DisplayMacroRatio(dailyTotalsCalculator);
            DisplayDailyGrid(dbFoodItems);
            ClearUserInput();
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
            ratioLabel.Text = string.Format("Proteins: {0:0.00}% <br/> Carbs: {1:0.00}% <br/> Fats: {2:0.00}%",
               dailyTotalsCalculator.PercentProteins,
               dailyTotalsCalculator.PercentCarbs,
               dailyTotalsCalculator.PercentFats);
        }

        private void DisplayDailyGrid(DbSet<FoodItem> dbFoodItems)
        {
            foodGridView.DataSource = dbFoodItems
                .Where(p => DbFunctions.TruncateTime(p.DateEntered) == DateTime.Today)
                .OrderBy(p => p.DateEntered).ToList();
            foodGridView.DataBind();
        }

        private void ClearUserInput()
        {
            newNameTextBox.Text = "";
            newCaloriesTextBox.Text = "";
            newProteinsTextBox.Text = "";
            newCarbsTextBox.Text = "";
            newFatsTextBox.Text = "";
        }
    }
}