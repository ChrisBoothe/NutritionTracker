using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using System.Web.UI.DataVisualization.Charting;

namespace NutritionTracker
{
    public partial class Default : System.Web.UI.Page
    { 
        //Add method to generate PDF report
        //Add unit testing class
        //Data must display even after input validation fails

        protected void Page_Load(object sender, EventArgs e)
        {   
            if (!Page.IsPostBack)
            {
                var foodItemManager = new FoodItemManager();                                
                foodItemManager.Calculator.SumMacros(foodItemManager.DbFoodItems);
                foodItemManager.Calculator.DetermineRatio(foodItemManager.DbFoodItems);
                DisplayData(foodItemManager.Calculator, foodItemManager.DbFoodItems);
            }

            errorLabel.Text = "";            
        }

        protected void addButton_Click(object sender, EventArgs e)
        { 
            var foodItemManager = new FoodItemManager();

            if (!foodItemManager.ValidateUserInput(foodItemManager.NewFoodItem, 
                newNameTextBox.Text, 
                newCaloriesTextBox.Text,
                newProteinsTextBox.Text, 
                newCarbsTextBox.Text, 
                newFatsTextBox.Text))
            {
                errorLabel.Text = "You must use a valid name and number!";
                return;
            }
            
            foodItemManager.UpdateDatabase();
            foodItemManager.Calculator.SumMacros(foodItemManager.DbFoodItems);
            foodItemManager.Calculator.DetermineRatio(foodItemManager.DbFoodItems);
            DisplayData(foodItemManager.Calculator, foodItemManager.DbFoodItems);
        }        

        private void DisplayData(DailyTotalsCalculator dailyTotalsCalculator, DbSet<FoodItem> dbFoodItems)
        {
            DisplayDailyTotals(dailyTotalsCalculator);
            DisplayMacroRatio(dailyTotalsCalculator);
            DisplayDailyGrid(dbFoodItems);
            DisplayRatioChart(dailyTotalsCalculator);
            DisplayTrendChart(dailyTotalsCalculator, dbFoodItems);
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
            ratioProteinsLabel.Text = string.Format("{0:0.00}%", dailyTotalsCalculator.PercentProteins);               
            ratioCarbsLabel.Text = string.Format("{0:0.00}%", dailyTotalsCalculator.PercentCarbs);
            ratioFatsLabel.Text = string.Format("{0:0.00}%", dailyTotalsCalculator.PercentFats);
        }

        private void DisplayRatioChart(DailyTotalsCalculator dailyTotalsCalculator)
        {
            Series series = Chart1.Series["Series1"];
            series.Points.AddXY("Proteins", dailyTotalsCalculator.PercentProteins);
            series.Points.AddXY("Carbs", dailyTotalsCalculator.PercentCarbs);
            series.Points.AddXY("Fats", dailyTotalsCalculator.PercentFats);
        }

        private void DisplayTrendChart(DailyTotalsCalculator dailyTotalsCalculator, DbSet<FoodItem> dbFoodItems)
        {
            Series series2 = Chart2.Series["Series1"];            
            for (int i = 0; i > -7; i--)
            {
                series2.Points.AddXY(DateTime.Today.AddDays(i),
                dailyTotalsCalculator.DetermineCalorieTrend(dbFoodItems, DateTime.Today.AddDays(i)));
            }
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