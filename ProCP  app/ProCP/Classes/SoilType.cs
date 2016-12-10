using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCP.Classes
{
    class SoilType
    {
        private string name;
        private decimal waterRentention;
        private decimal startingSoilNutrition;

        public SoilType(string name, decimal waterRentention, decimal startingSoilNutrition) {
            this.name = name;
            this.waterRentention = waterRentention;
            this.startingSoilNutrition = startingSoilNutrition;
        }
        
        
        public string GetName() { return name; }
        public decimal GetWaterRentention() { return waterRentention; }
        public decimal GetStartingSoilNutrition() { return startingSoilNutrition; }
        
    }
}
