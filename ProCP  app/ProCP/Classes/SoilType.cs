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
        private decimal Water_Loose_Rate;
        private decimal Nutrition_Loose_Rate;

        public SoilType(string name, decimal maximum_water, decimal waterlooserate, decimal maximum_nutrition, decimal nutritionlooserate) {
            this.name = name;
            this.Maximum_Water = maximum_water;
            this.Water_Loose_Rate = waterlooserate;
            this.Maxium_Nutrition = maximum_nutrition;
            this.Nutrition_Loose_Rate = nutritionlooserate;
        }
        
        
        public string GetName() { return name; }
        public decimal GetWaterLooseRate() { return Water_Loose_Rate; }
        public decimal GetMaximumNutrition() { return Maxium_Nutrition; }
        public decimal GetMaximumWater() { return Water_Loose_Rate;}
        public decimal GetNutritionLooseRate() { return Nutrition_Loose_Rate; }
        
    }
}
