﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCP.Classes
{
    class PlotWeek
    {
        public bool isEmpty { get; set; }
        public decimal WaterSaturation { get; set; }
        public decimal SaturationLooseRate { get; set; }
        public decimal SoilNutrition { get; set; }
        public decimal MaximumSoilNutrition { get; set; }
        public Weather weather;
        private Crop crop;
        public bool imageChange { get; set; }
        public int imageNumber { get; set; }

        
        public  PlotWeek()
        {
            this.isEmpty = true;
            this.imageChange = false;
            this.imageNumber = -1;
        }
        
        public void setCrop(Crop crop)
        {
            this.crop = crop;
            this.isEmpty = false;
        }

       
        public void DeleteCrop()
        {
            this.crop = null;
            isEmpty = true;
        }
        public Crop getCrop()
        {
            if (!isEmpty)
            {
                return crop;
            }
            else return null;
        }
        public string getCropNameInPlot()
        {
            if (isEmpty)
            {
                return "";
            }
            else
            {
                return this.crop.GetCropName();
            }
        }
        public decimal getTemperture()
        {
            return weather.GetTemp();
        }

    }
}
