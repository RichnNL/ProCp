using System;
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
        private Weather weather;
        private Crop crop;
        public bool imageChange { get; set; }

        
        public  PlotWeek()
        {
            this.isEmpty = true;
            this.imageChange = false;
        }
        
        public void setCrop(Crop crop)
        {
            this.crop = crop;
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

    }
}
