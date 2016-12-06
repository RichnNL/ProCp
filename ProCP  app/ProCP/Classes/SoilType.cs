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
        private int saturation; // maximum water it can hold

        //private  int maximumSoilNutrition;
        private int startingSoilNutrition;

        public SoilType(string name, int saturation, int startingSoilNutrition) { }
        
        
        public string GetName() { return null; }
        public int GetSaturation() { return 0; }
        public int GetStartingSoilNutrition() { return 0; }
        public int GetMAximumSoilNutrition() { return 0; }
    }
}
