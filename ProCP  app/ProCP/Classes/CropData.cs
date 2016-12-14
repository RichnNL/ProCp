using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCP.Classes
{
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

        private bool isEmpty ;

        private string SoilType;

        private string plotPosition;

        private int numberOfCrops;

        private int numberOfCropsFinished;

        public CropData(string health, string soilType, string plotposition)
        {
            this.Crop_Health = health;
            this.isEmpty = true;
            this.SoilType = soilType;
            this.plotPosition = plotposition;
        }
        public CropData(DateTime begin, DateTime end, bool Alive, string health, string soilType, string plotposition)
        {
            this.beginDate = begin;
            this.endDate = end;
            this.isAlive = Alive;
            this.Crop_Health = health;
            this.isEmpty = false;
            this.SoilType = soilType;
            this.plotPosition = plotposition;
        }
        public CropData(DateTime begin, DateTime end, bool Alive, string health, decimal watercosts, decimal fertilizer, string soilType, string plotposition)
        {
            this.beginDate = begin;
            this.endDate = end;
            this.isAlive = Alive;
            this.Crop_Health = health;
            this.waterCosts = watercosts;
            this.fertilizerCosts = fertilizer;
            this.isEmpty = false;
            this.SoilType = soilType;
            this.plotPosition = plotposition;
        }
        public CropData(DateTime begin, DateTime end, bool Alive, string health, decimal watercosts, decimal fertilizer,decimal totalcosts, string soilType, string plotposition)
        {
            this.beginDate = begin;
            this.endDate = end;
            this.isAlive = Alive;
            this.Crop_Health = health;
            this.waterCosts = watercosts;
            this.fertilizerCosts = fertilizer;
            this.totalCosts = totalcosts;
            this.isEmpty = false;
            this.SoilType = soilType;
            this.plotPosition = plotposition;
        }
        public CropData(DateTime begin, DateTime end, bool Alive, string health, decimal watercosts, decimal fertilizer, decimal totalcosts,int yield, string soilType, string plotposition)
        {
            this.beginDate = begin;
            this.endDate = end;
            this.isAlive = Alive;
            this.Crop_Health = health;
            this.waterCosts = watercosts;
            this.fertilizerCosts = fertilizer;
            this.totalCosts = totalcosts;
            this.yield = yield;
            this.isEmpty = false;
            this.SoilType = soilType;
            this.plotPosition = plotposition;
        }
        public CropData(DateTime begin, DateTime end, bool Alive, string health, int yield, string soilType, string plotposition)
        {
            this.beginDate = begin;
            this.endDate = end;
            this.isAlive = Alive;
            this.Crop_Health = health;
            this.yield = yield;
            this.isEmpty = false;
            this.SoilType = soilType;
            this.plotPosition = plotposition;
        }
        public CropData(DateTime begin, DateTime end, bool Alive, string health, decimal totalcosts, int yield, string soilType, string plotposition)
        {
            this.beginDate = begin;
            this.endDate = end;
            this.isAlive = Alive;
            this.Crop_Health = health;
            this.totalCosts = totalcosts;
            this.yield = yield;
            this.isEmpty = false;
            this.SoilType = soilType;
            this.plotPosition = plotposition;
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
        public bool getIsEmpty()
        {
            return isEmpty;
        }
        public string getPlotID()
        {
            return plotPosition;
        }

        public string getSoilType()
        {
            return SoilType;
        }
       

    }
}
