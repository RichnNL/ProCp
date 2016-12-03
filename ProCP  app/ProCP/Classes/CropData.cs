using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCP.Classes
{
    class CropData
    {
        private int yield;
       

        private decimal totalCosts;
        public decimal totalCosts { get; set; }

        private decimal waterCosts;
        public decimal waterCosts { get; set; }

        private decimal fertilizerCosts;
        public decimal fertilizerCosts { get; set; }

        private DateTime beginDate;
        public DateTime BeginDate { get; set; }

        private DateTime endDate;
        public DateTime EndDate { get; set; }

        public CropData(DateTime begin, DateTime end)
        { }

        public DateTime GetBeginDate() { return DateTime.Now; }
        public DateTime GetEndDate() { return DateTime.Now; }
        public int GetYield()
        {
            return 0; 
        }
        public decimal GetTotalCost()
        {
            return 0;
        }

        public decimal GetWaterCost()
        {
            return 0;
        }

        public decimal GetFertilizerCost()
        {
            return 0;
        }

        ///crop health?????
       

    }
}
