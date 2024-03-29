﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace NutritionTracker
{
    public class DailyTotalsCalculator
    {                        
        public int TotalCalories { get; set; }
        public int TotalProteins { get; set; }
        public int TotalCarbs { get; set; }
        public int TotalFats { get; set; }        
        public double PercentProteins { get; set; }
        public double PercentCarbs { get; set; }
        public double PercentFats { get; set; }

        private double _totalMacros;

        public DailyTotalsCalculator()
        {
            TotalCalories = 0;
            TotalProteins = 0;
            TotalCarbs = 0;
            TotalFats = 0;            
            PercentProteins = 0;
            PercentCarbs = 0;
            PercentFats = 0;
            _totalMacros = 0;            
        }

        public void SumMacros(DbSet<FoodItem> dbFoodItems)
        {
            foreach (var foodItem in dbFoodItems)                            
                if (foodItem.DateEntered.Date == DateTime.Today)
                {
                    TotalCalories += foodItem.Calories;
                    TotalProteins += foodItem.Proteins;
                    TotalCarbs += foodItem.Carbs;
                    TotalFats += foodItem.Fats;
                }   
        }

        public void DetermineRatio()
        {
            _totalMacros = ((TotalProteins * 4) + (TotalCarbs * 4) + (TotalFats * 9));
            PercentProteins = ((TotalProteins * 4) / _totalMacros) * 100;
            PercentCarbs = ((TotalCarbs * 4) / _totalMacros) * 100;
            PercentFats = ((TotalFats * 9) / _totalMacros) * 100;            
        }

        public int DetermineCalorieTrend(DbSet<FoodItem> dbFoodItems, DateTime dateTime)
        {
            var totalCalories = 0;

            foreach (var foodItem in dbFoodItems)            
                if (foodItem.DateEntered.Date == dateTime)                
                    totalCalories += foodItem.Calories;
            
            return totalCalories;
        }
    }
}