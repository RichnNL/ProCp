using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCP.Classes
{
    class Crop
    {
        private String cropName;
        private int maturityLength;
        private int waterMinimum;
        private int waterMaximum;
        private int waterThirst;
        //private int sunLightMinimum;
        private int neededNutrition;
        //private int neededSunLight;
        private int nutritionRate;
        private int temperature;
        private int cropYield;
        private String cropSeason;

        public Crop(String name, int maturity, int wmin, int wmax, int thirst, int nr, int temp, String season, int yield)
        { }
        public int GetMaturityLength() { return 0; }
        public int GetWaterMinimum() { return 0; }
        public int GetWaterMaximum() { return 0; }
        public int GetThirst() { return 0; }
        //public int GetSunlightMinimum() { return 0; }
        public int GetNeededNutrition() { return 0; }
        public int GetNeededSunLight() { return 0; }
       // public Images GetImage(int ImageNum) { }





    }
}
