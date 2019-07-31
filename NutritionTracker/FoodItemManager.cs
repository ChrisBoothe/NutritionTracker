using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NutritionTracker
{
    public class FoodItemManager
    {
        public static void RemoveOldEntries(System.Data.Entity.DbSet<FoodItem> dbFoodItems)
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