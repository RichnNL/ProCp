using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCP.Classes
{
    class Crop
    {
        private string cropName;
        private int maturityLength;
        private decimal waterMinimum;
        private decimal waterMaximum;
        private decimal waterThirst;
        private decimal neededNutrition;
        private decimal nutritionRate;
        private decimal temperature;
        private int cropYield;
        private string cropSeason;
        public List<Crop_Week> weeks;
        public Crop(string name, int maturity, decimal wmin, decimal wmax, decimal thirst,decimal nN, decimal nr, decimal temp, string season, int yield)
        {
            this.cropName = name;
            this.maturityLength = maturity;
            this.waterMinimum = wmin;
            this.waterMaximum = wmax;
            this.waterThirst = thirst;
            this.neededNutrition = nN;
            this.temperature = temp;
            this.cropSeason = season;
            this.cropYield = yield;
            weeks = new List<Crop_Week>();
            for(int i = 0; i < maturityLength; i++)
            {
                weeks.Add(new Crop_Week());
            }
        }
        public Crop(string name) { }
        public int GetMaturityLength() { return maturityLength; }
        public decimal GetWaterMinimum() { return waterMinimum; }
        public decimal GetWaterMaximum() { return waterMaximum; }
        public decimal GetThirst() { return waterThirst; }
        public decimal GetTemperature() { return temperature; }
        public string GetSeason() { return cropSeason; }
        public int GetCropYield() { return cropYield; }
       
        public decimal GetNeededNutrition() { return 0; }
        
      





    }
}
