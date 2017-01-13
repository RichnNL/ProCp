using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCP.Classes
{
    [Serializable]
    class CropData
    {
        private int yield = 0;
       

        private decimal totalCosts = 0;
        

        private decimal waterCosts = 0;
        

        private decimal fertilizerCosts = 0;
        

        private DateTime beginDate;
        

        private DateTime endDate;

        private bool isAlive;

        private string Crop_Health = "";

        private decimal profits = 0;
        private int idealYield = 0;
        private string CropName;
        private string CropLocation;
       

       
        public CropData(string CropName, string CropLocation, DateTime begin, DateTime end, bool Alive, string health)
        {
            this.beginDate = begin;
            this.endDate = end;
            this.isAlive = Alive;
            this.Crop_Health = health;
            this.CropName = CropName;
            this.CropLocation = CropLocation;
         
        }
        public CropData(string CropName, string CropLocation, DateTime begin, DateTime end, bool Alive, string health, decimal watercosts, decimal fertilizer)
        {
            this.beginDate = begin;
            this.endDate = end;
            this.isAlive = Alive;
            this.Crop_Health = health;
            this.waterCosts = watercosts;
            this.fertilizerCosts = fertilizer;
            this.CropName = CropName;
            this.CropLocation = CropLocation;

        }
        public CropData(string CropName, string CropLocation, DateTime begin, DateTime end, bool Alive, string health, decimal watercosts, decimal fertilizer,decimal totalcosts)
        {
            this.beginDate = begin;
            this.endDate = end;
            this.isAlive = Alive;
            this.Crop_Health = health;
            this.waterCosts = watercosts;
            this.fertilizerCosts = fertilizer;
            this.totalCosts = totalcosts;
            this.CropName = CropName;
            this.CropLocation = CropLocation;

        }
        public CropData(string CropName, string CropLocation, DateTime begin, DateTime end, bool Alive, string health, decimal watercosts, decimal fertilizer, decimal totalcosts,int yield)
        {
            this.beginDate = begin;
            this.endDate = end;
            this.isAlive = Alive;
            this.Crop_Health = health;
            this.waterCosts = watercosts;
            this.fertilizerCosts = fertilizer;
            this.totalCosts = totalcosts;
            this.yield = yield;
            this.CropName = CropName;
            this.CropLocation = CropLocation;

        }
        public CropData(string CropName, string CropLocation, DateTime begin, DateTime end, bool Alive, string health, decimal watercosts, decimal fertilizer, decimal totalcosts, int yield,decimal profit)
        {
            this.beginDate = begin;
            this.endDate = end;
            this.isAlive = Alive;
            this.Crop_Health = health;
            this.waterCosts = watercosts;
            this.fertilizerCosts = fertilizer;
            this.totalCosts = totalcosts;
            this.yield = yield;
            this.profits = profit;
            this.CropName = CropName;
            this.CropLocation = CropLocation;

        }
        public CropData(string CropName, string CropLocation, DateTime begin, DateTime end, bool Alive, string health, decimal watercosts, decimal fertilizer, decimal totalcosts, int yield,int idealyield, decimal profit)
        {
            this.beginDate = begin;
            this.endDate = end;
            this.isAlive = Alive;
            this.Crop_Health = health;
            this.waterCosts = watercosts;
            this.fertilizerCosts = fertilizer;
            this.totalCosts = totalcosts;
            this.yield = yield;
            this.profits = profit;
            this.idealYield = idealyield;
            this.CropName = CropName;
            this.CropLocation = CropLocation;

        }
        public CropData(string CropName, string CropLocation, DateTime begin, DateTime end, bool Alive, string health, int yield)
        {
            this.beginDate = begin;
            this.endDate = end;
            this.isAlive = Alive;
            this.Crop_Health = health;
            this.yield = yield;
            this.CropName = CropName;
            this.CropLocation = CropLocation;

        }
        public CropData(string CropName, string CropLocation, DateTime begin, DateTime end, bool Alive, string health, decimal totalcosts, int yield)
        {
            this.beginDate = begin;
            this.endDate = end;
            this.isAlive = Alive;
            this.Crop_Health = health;
            this.totalCosts = totalcosts;
            this.yield = yield;
            this.CropName = CropName;
            this.CropLocation = CropLocation;

        }

        public string getCropName()
        {
            return CropName;
        }
        public string getCropLocation()
        {
           return CropLocation;
        }
        public decimal getProfits()
        {
            return profits;
        }

        public DateTime GetBeginDate()
        {

            return beginDate;
        }
        public DateTime GetEndDate()
        {
            return endDate;
        }
        public int GetYield()
        {
            return yield ; 
        }
        public decimal GetTotalCost()
        {
            return totalCosts;
        }

        public decimal GetWaterCost()
        {
            return waterCosts;
        }

        public decimal GetFertilizerCost()
        {
            return fertilizerCosts;
        }

        public string getHealth()
        {
            return Crop_Health;
        }
        public bool getIsAlive()
        {
            return isAlive;
        }
        public int getIdealYield()
        {
            return idealYield;
        }
       
    }
}
