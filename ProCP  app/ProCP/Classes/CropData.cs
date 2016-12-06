﻿using System;
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
        public decimal TotalCosts { get; set; }

        private decimal waterCosts;
        public decimal WaterCosts { get; set; }

        private decimal fertilizerCosts;
        public decimal FertilizerCosts { get { return fertilizerCosts; } set { value = fertilizerCosts; } }

        private DateTime beginDate;
        public DateTime BeginDate { get { return beginDate; } set { value = beginDate; } }

        private DateTime endDate;
        public DateTime EndDate { get { return endDate; } set { endDate = value; } }

        public CropData(DateTime begin, DateTime end)
        {

        }

        public DateTime GetBeginDate()
        {

            return BeginDate;
        }
        public DateTime GetEndDate()
        {
            return EndDate;
        }
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