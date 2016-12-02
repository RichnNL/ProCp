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
        public int WaterSaturation { get; set; }
        public int SaturationLooseRate { get; set; }
        public int SoilNutrition { get; set; }
        public int MaximumSoilNutrition { get; set; }
        private Weather weather;
        private Crop crop;

        // constructor
        public  PlotWeek()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="crop"></param>
        public void setCrop(Crop crop)
        {
           

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="crop"></param>
        public void DeleteCrop(Crop crop)
        {

        }

    }
}
