using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCP.Classes
{
    [Serializable]
    class SoilType
    {
        private string name;
        private decimal Maximum_Water;
        private decimal Maxium_Nutrition;
        private decimal Water_Lose_Rate;
        private decimal Nutrition_Lose_Rate;

        public SoilType(string name, decimal maximum_water, decimal waterloserate, decimal maximum_nutrition, decimal nutritionloserate) {
            this.name = name;
            this.Maximum_Water = maximum_water;
            this.Water_Lose_Rate = waterloserate;
            this.Maxium_Nutrition = maximum_nutrition;
            this.Nutrition_Lose_Rate = nutritionloserate;
        }
        
        
        public string GetName() { return name; }
        public decimal GetWaterLoseRate() { return Water_Lose_Rate; }
        public decimal GetMaximumNutrition() { return Maxium_Nutrition; }
        public decimal GetMaximumWater() { return Maximum_Water;}
        public decimal GetNutritionLoseRate() { return Nutrition_Lose_Rate; }
        
    }
}
