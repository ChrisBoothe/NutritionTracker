//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NutritionTracker
{
    using System;
    using System.Collections.Generic;
    
    public partial class FoodItem
    {
        public System.Guid FoodEntryId { get; set; }
        public string Name { get; set; }
        public int Calories { get; set; }
        public int Proteins { get; set; }
        public int Carbs { get; set; }
        public int Fats { get; set; }
        public System.DateTime DateEntered { get; set; }
    }
}
