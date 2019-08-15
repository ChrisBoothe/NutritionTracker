using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace NutritionTracker
{
    public class FoodItemManager
    {

        public FoodEntities Db { get; set; }
        public DbSet<FoodItem> DbFoodItems { get; set; }
        public FoodItem NewFoodItem { get; set; }
        public DailyTotalsCalculator Calculator { get; set; }

        public FoodItemManager()
        {
            Db = new FoodEntities();
            DbFoodItems = Db.FoodItems;
            NewFoodItem = new FoodItem();
            Calculator = new DailyTotalsCalculator();
        }

        public bool ValidateUserInput(FoodItem newFoodItem, string nameText, string caloriesText, 
            string proteinsText, string carbsText, string fatsText)
        {            
            int newCalories;
            int newProteins;
            int newCarbs;
            int newFats;

            if (string.IsNullOrEmpty(nameText) ||
                !int.TryParse(caloriesText, out newCalories) ||
                !int.TryParse(proteinsText, out newProteins) ||
                !int.TryParse(carbsText, out newCarbs) ||
                !int.TryParse(fatsText, out newFats))
            {
                return false;
            }

            else
            {
                AssignUserInput(newFoodItem, nameText, newCalories, newProteins, newCarbs, newFats);
                return true;
            }
        }

        private void AssignUserInput(FoodItem newFoodItem, string newName, int newCalories, 
            int newProteins, int newCarbs, int newFats)
        {
            newFoodItem.FoodEntryId = Guid.NewGuid();
            newFoodItem.Name = newName;
            newFoodItem.Calories = newCalories;
            newFoodItem.Proteins = newProteins;
            newFoodItem.Carbs = newCarbs;
            newFoodItem.Fats = newFats;
            newFoodItem.DateEntered = DateTime.Now;
        }

        public void UpdateDatabase()
        {
            RemoveOldEntries(DbFoodItems);
            DbFoodItems.Add(NewFoodItem);            
            Db.SaveChanges();
        }

        private void RemoveOldEntries(DbSet<FoodItem> dbFoodItems)
        {
            foreach (var foodItem in dbFoodItems)
            {
                TimeSpan weekTimeFrame = DateTime.Today.Subtract(foodItem.DateEntered);

                if (weekTimeFrame.TotalDays > 7)
                {
                    dbFoodItems.Remove(foodItem);
                }
            }
        }
    }
}