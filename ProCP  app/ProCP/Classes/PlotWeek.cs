using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCP.Classes
{
    [Serializable]
    class PlotWeek
    {
        public bool isEmpty { get; set; }
        public decimal Water { get { return Water; } set { Water = value; if (Water < 0) { Water = 0; } ;} }
        public decimal SoilNutrition { get { return SoilNutrition; } set { SoilNutrition = value; if (SoilNutrition < 0) { SoilNutrition = 0; }; } }
       
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
