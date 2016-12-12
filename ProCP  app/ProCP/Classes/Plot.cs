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
        public List<PlotWeek> plotWeeks;
        private int hasCrop;


        public Plot(string PlotId,SoilType soiltype,int intialWeeks)
        {
            this.PlotId = PlotId;
            this.soiltype = soiltype;
            this.hasCrop = 0;
            plotWeeks = new List<PlotWeek>();
            for(int i = 0; i < intialWeeks; i++)
            {
                plotWeeks.Add(new PlotWeek());
            }
            setSoilType(this.soiltype);
        }
        
     
       
        public bool AddCrop(Crop crop)
        {
            int length = crop.GetMaturityLength();
            if (canInsertPlot(length))
            {
                int week = RCAEA.simulation.CurrentWeek;
                for (int i = 0; i < length; i++, week++)
                {
                    plotWeeks[week].setCrop(crop);
                }
                hasCrop++ ;
                CalBeginToEnd();
                return true;
                
            }
            else

                return false;
        }
        public bool RemoveCrop(int RemoveAtWeek)
        {
            Crop crop;
            int cropSetAtWeek;
           if(isNotEmptyAtSpecificWeek(RemoveAtWeek,out crop,out cropSetAtWeek))
            {
                int maturity = crop.GetMaturityLength();
                for (int i = 0; i < maturity; i++,cropSetAtWeek++)
                {
                    plotWeeks[cropSetAtWeek].DeleteCrop();
                }
                hasCrop--;
                CalBeginToEnd();
                return true;
            }
            else
            {
                return false;
            }

        }

        
        public bool RemoveAllCrop(List<int> getploposition) { return false; }
        public CropData GetCurrentCropData() { return null; }
        public CropData GetCropSummary() { return null; }
        public CropData GetCropDataByDate(DateTime d) { return null; }
       
        public void NurishLand() { }

        public void Manageweeks()
        {
            int nbrWeeks = RCAEA.simulation.GetNumberOfWeeks();
            int pweeks = plotWeeks.Count;
            if(nbrWeeks != pweeks)
            {
                List<PlotWeek> temp = new List<PlotWeek>();
                for(int i = 1; i < nbrWeeks; i++)
                {
                    temp.Add(new PlotWeek());
                }
                plotWeeks = temp;
                setSoilType(this.soiltype);
            }
           
        }
  
      
        public void CalBeginToEnd() {
            if(hasCrop == 0)
            {
                setSoilType(this.soiltype);
                // resets the Plot
            }
            else
            {
                //to do (only caluclates maturity for now)
                int startWeek = 0;
                for(int j = 0; j < plotWeeks.Count; j ++)
                {
                    // If null then has reached the end of PlotWeeks
                    if (!plotWeeks[startWeek].isEmpty)
                    {
                        Crop c = plotWeeks[startWeek].getCrop();
                        int end = c.GetMaturityLength();
                        for(int i = 0; i < end; i++,startWeek++,j++)
                        {
                            CalCurrentDate(startWeek, i,c);
                        }
                    }
                    else
                    {
                        startWeek++;
                    }
                }
               

            }
        }
        private void CalCurrentDate(int week,int CropMaturity,Crop crop) {
            // setting week entered based on last week
            if(isNotEmptyAtSpecificWeek(week))
            {
                // run plotWeekCalculations
                if(CropMaturity == 0)
                {
                    //if Crop is being intialized
                    crop.weeks[CropMaturity].maturity = 0;
                    plotWeeks[week].imageNumber = 0;
                    plotWeeks[week].imageChange = true;
                }
                else if(crop.GetMaturityLength() > crop.weeks[CropMaturity].maturity)
                {
                    crop.weeks[CropMaturity].maturity = crop.weeks[CropMaturity - 1].maturity + 1; ;
                }
                
            }
            
        }

        private void setSoilType(SoilType soiltype)
        {
            this.soiltype = soiltype;
            this.plotWeeks[0].MaximumSoilNutrition = soiltype.GetStartingSoilNutrition();
            this.plotWeeks[0].SoilNutrition = soiltype.GetStartingSoilNutrition();
            if (hasCrop != 0)
            {
                CalBeginToEnd();
            }
        }
        private bool canInsertPlot(int maturity)
        {
            //Checks if Enough Weeks Available and if the plot is empty
            int now = RCAEA.simulation.CurrentWeek;
            int then = maturity + now;
            if (RCAEA.simulation.GetNumberOfWeeks() - then > 0)
            {
                for(int c = now; c <= then; c++)
                {
                    if (!plotWeeks[c].isEmpty)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        private PlotWeek getCurrentPlotWeek()
        {
            return plotWeeks[RCAEA.simulation.CurrentWeek];
        }

        private bool isNotEmptyAtSpecificWeek(int week)
        {
            if (plotWeeks[week].isEmpty)
            {

                return false;
            }
            else{
                return true;
            }
        }
        private bool isNotEmptyAtSpecificWeek(int week, out Crop getCrop)
        {
            getCrop = null;
            if (plotWeeks[week].isEmpty)
            {
                return false;
            }
            getCrop = plotWeeks[week].getCrop();
            return true;
        }
        private bool isNotEmptyAtSpecificWeek(int week, out Crop getCrop, out int weekCropWasSet)
        {
            getCrop = null;
            weekCropWasSet = -1;
            if (plotWeeks[week].isEmpty)
            {
                return false;
            }
            if(week == 0)
            {
                weekCropWasSet = 0;
            }
            getCrop = plotWeeks[week].getCrop();
            while (week != 0)
            {
                Crop test = plotWeeks[week - 1].getCrop();
                if(test == null || test != getCrop)
                {
                    weekCropWasSet = week;
                }
                else
                {
                    week--;
                }
            }
            return true;
            
        }

        

    }
}
