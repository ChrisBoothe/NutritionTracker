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