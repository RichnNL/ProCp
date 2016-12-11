using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProCP.Classes
{
    class Plot
    {
        public string PlotId;
        private SoilType soiltype;
        private List<PlotWeek> plotWeeks;
        private Database db;


        public Plot(string PlotId,SoilType soiltype)
        {
            plotWeeks = new List<PlotWeek>();
            
            Manageweeks();


        }
     
       
        public bool AddCrop(Crop crop)
        {
            int length = crop.GetMaturityLength();
            if (canInsertPlot(length))
            {
                int week = RCAEA.simulation.getCurrentWeek();
                for (int i = 0; i < length; i++, week++)
                {
                    plotWeeks[week].setCrop(crop);
                }
                return true;
                
            }
            else

                return false;
        }
        public bool RemoveCrop(Crop crop, DateTime date)
        {
           Crop remove = plotWeeks[RCAEA.simulation.getSpecificWeek(date)].getCrop();
            if( crop.GetCropName() == remove.GetCropName())
            {
                //remove.weeks[]
                //to do
                return true;
            }
            else
            {
                return false;
            }

        }

     
        public bool RemoveCrop(int getploposition) { return false; }
        public bool RemoveAllCrop(List<int> getploposition) { return false; }
        public CropData GetCurrentCropData() { return null; }
        public CropData GetCropSummary() { return null; }
        public CropData GetCropataByDate(DateTime d) { return null; }
       
        public void DrawSelf() { }
        public void NurishLand() { }

        public void Manageweeks()
        {
            int nbrWeeks = RCAEA.simulation.GetNumberOfWeeks();
            for (int i = 0; i < nbrWeeks; i++)
            {
                plotWeeks.Add(new PlotWeek());
            }
        }
  
      
        public void CalBeginToEnd() { }
        public void CalCurrentDate() { }

        private void setSoilType(SoilType soiltype)
        {
            this.soiltype = soiltype;
            this.plotWeeks[0].MaximumSoilNutrition = soiltype.GetStartingSoilNutrition();
            this.plotWeeks[0].SoilNutrition = soiltype.GetStartingSoilNutrition();
            CalBeginToEnd();
        }
        private bool canInsertPlot(int maturity)
        {
            DateTime now = RCAEA.simulation.CurrentDate;
            DateTime then = now.AddDays(maturity * 7);
            if (RCAEA.simulation.EndDate > then)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private PlotWeek getCurrentPlotWeek()
        {
            return plotWeeks[RCAEA.simulation.getCurrentWeek()];
        }




    }
}
