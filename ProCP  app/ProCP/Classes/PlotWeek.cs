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
        public decimal Water { get { return water; } set { water = value; if (water < 0) { water = 0; } ;} }
        private decimal water;
        public decimal SoilNutrition { get { return soilnutrition; } set { soilnutrition = value; if (soilnutrition < 0) { soilnutrition = 0; }; } }
        private decimal soilnutrition;
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
