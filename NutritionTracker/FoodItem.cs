using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NutritionTracker
{
    public class FoodItem
    {
        public string Name { get; set; }        
        public int Calories { get; set; }
        public int Proteins { get; set; }
        public int Carbs { get; set; }
        public int Fats { get; set; }
        public DateTime DateEntered { get; set; }        
    }

}