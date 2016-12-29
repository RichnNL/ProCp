using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCP.Classes
{
    class Crop
    {
        public decimal currentwater;
        private string cropName;
        private int maturityLength;
        private decimal waterMinimum;
        private decimal waterMaximum;
        private decimal waterThirst;
        private decimal neededNutrition;
        private decimal temperature;
        private int cropYield;
        private string cropSeason;
        public List<Crop_Week> weeks;
        public Crop(string name, int maturity, decimal wmin, decimal wmax, decimal thirst,decimal nN, decimal temp, string season, int yield)
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
            for (int i = 0; i < maturityLength; i++)
            {
                weeks.Add(new Crop_Week());
                weeks[i].maturity = i;
                weeks[i].Health = 100;
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
        public string GetCropName()
        {
            return this.cropName;
        }
        public void setCropHealth(int addOrSubtractHealthPoints,int atWeek)
        {   if(atWeek < maturityLength)
            {
                for (int i = atWeek; i< maturityLength; i++)
                {
                    weeks[i].Health += addOrSubtractHealthPoints;
                    
                }
            }
           
        }
       public void setInitalValues()
        {
            List<Crop_Week> temp = new List<Crop_Week>();
            for (int i = 0; i < maturityLength; i++)
            {
                temp.Add(new Crop_Week());
                temp[i].maturity = i;
                temp[i].Health = 100;
            }
            weeks = temp;
        }
        
        
      





    }
}
