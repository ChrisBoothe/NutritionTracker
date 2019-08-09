using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace NutritionTracker
{
    public class FoodItemManager
    {
        public static void RemoveOldEntries(DbSet<FoodItem> dbFoodItems)
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