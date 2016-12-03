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
        private int SaturationLoseRate;
        private  int maximumSoilNutrition;
        private int startingSoilNutrition;

        public SoilType(string name, int slr, int msn, int ssn) { }
        public string GetName() { return null; }
        public int GetSaturationLoseRate() { return 0; }
        public int GetStartingSoilNutrition() { return 0; }
        public int GetMAximumSoilNutrition() { return 0; }
    }
}
