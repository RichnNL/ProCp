﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProCP.Classes
{
    [Serializable]
    class Plot
    {
        public string PlotId;
        private SoilType soiltype;
        public List<PlotWeek> plotWeeks;
        private int hasCrop;
        private int cropsHarvested;
        [field:NonSerialized]
        private Simulation simulation;


        public Plot(string PlotId,SoilType soiltype,int intialWeeks,Simulation simulation)
        {
            this.PlotId = PlotId;
            this.soiltype = soiltype;
            this.hasCrop = 0;
            this.cropsHarvested = 0;
            this.simulation = simulation;
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
                int week = simulation.CurrentWeek;
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
        /// <summary>
        /// Calculate Soil Factor
        /// </summary>
        private void CalculateSoilFactor()
        {
           
        }

        public bool RemoveAllCrop(List<int> getploposition) { return false; }
        public CropData GetCurrentCropData() {
            CropData cropdata;
            int now = simulation.CurrentWeek;
            Crop crop;
            int beginWeek;
            if (isNotEmptyAtSpecificWeek(now,out crop, out beginWeek))
            {
                bool alive;
                int health = plotWeeks[now].getCrop().weeks[now - beginWeek].Health;
                string health_details = "Crop " + crop.GetCropName();
                if ( health > 1){
                    alive = false;
                }
                else
                {
                    alive = true;
                }
                if(health > 90)
                {
                    health_details += " is very healthly.";
                }
                else if(health < 90 && health > 50)
                {
                    health_details += " is need more care.";
                }
                else if(health < 50 && health > 20)
                {
                    health_details += " need more care.";
                }
                else
                {
                    health_details += " needs urgent care.";
                }

                cropdata = new CropData(simulation.weekToDate(beginWeek), simulation.weekToDate(beginWeek + crop.GetMaturityLength()), alive, health_details);
                return cropdata;
            }
            else
            {
                return null;  
            }

        }
        public CropData GetCropSummary() {
            
          
            return null;
        }
        public CropData GetCropDataByDate(DateTime d) { return null; }
       
        private void NurishLand(int week, int CropMaturity, Crop crop)
        {
            // Add Water From Rain
            AddWaterToSoil(week, plotWeeks[week].weather.GetRain());
            NurishWithWater(week, CropMaturity, crop);
        }
        private void NurishLand(int week)
        {
            // Add Water From Rain
            AddWaterToSoil(week, plotWeeks[week].weather.GetRain());
            
        }
        private void NurishWithWater(int week, int CropMaturity, Crop crop)
        {

            
        }
       
        private void AddWaterToSoil(int week, decimal waterAmount)
        {
            //Get the Water from the week before
            if (week != 0)
            {
                plotWeeks[week].Water = plotWeeks[week - 1].Water;
            }
            // Calculate the Water Loose Rate
            plotWeeks[week].Water -= soiltype.GetWaterLooseRate();
            

            //Add Water Amount
            plotWeeks[week].Water += waterAmount;
            if (plotWeeks[week].Water > soiltype.GetMaximumWater())
            {
                plotWeeks[week].Water = soiltype.GetMaximumWater();

            }


        }
       
     
        public void Manageweeks()
        {
            int nbrWeeks = simulation.GetNumberOfWeeks();
            int pweeks = plotWeeks.Count;
            if(nbrWeeks != pweeks)
            {
                List<PlotWeek> temp = new List<PlotWeek>();
                for(int i = 1; i < nbrWeeks; i++)
                {
                    temp.Add(new PlotWeek());
                }
                
                plotWeeks = temp;
                for(int i = 0; i < plotWeeks.Count; i++)
                {
                    setWeatherForWeek(i);
                }
                setSoilType(this.soiltype);
            }
           
        }
  
      
        public void CalBeginToEnd() {
            cropsHarvested = 0;
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
                   else if(j != 0 && plotWeeks[startWeek].isEmpty)
                    {
                        NurishLand(startWeek);
                        
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
                // run Caluclations for that week

                // First Cultivate Lands
                if(CropMaturity == 0)
                {
                    //if Crop is being intialized
                    
                    plotWeeks[week].imageNumber = 0;
                    plotWeeks[week].imageChange = true;
                }
                else if( CropMaturity == crop.GetMaturityLength() && crop.weeks[CropMaturity].Health > 0)
                {
                    if(plotWeeks[week +1] != null && plotWeeks[week + 1].isEmpty)
                    {
                        plotWeeks[week].imageChange = true;
                        plotWeeks[week].imageNumber = -1;
                    }
                    
                    cropsHarvested++;
                }
                else if(crop.weeks[CropMaturity].Health == 0 && crop.weeks[CropMaturity - 1].Health != 0)
                {
                    plotWeeks[week].imageChange = true;
                    plotWeeks[week].imageNumber = 5;
                }
                
            }
            
        }

        public void setSoilType(SoilType soiltype)
        {
            this.soiltype = soiltype;
            resetPlotWeeks();
          
            if (hasCrop != 0)
            {
                CalBeginToEnd();
            }
        }
        private void resetPlotWeeks()
        {
            for (int i = 0; i < plotWeeks.Count; i++)
            {
                if(i == 0)
                {
                    this.plotWeeks[i].SoilNutrition = soiltype.GetMaximumNutrition() / 3;
                }
                this.plotWeeks[i].SoilNutrition = 0;
                this.plotWeeks[i].imageNumber = -1;
                this.plotWeeks[i].imageChange = false;
               
            }
        }
        private bool canInsertPlot(int maturity)
        {
            //Checks if Enough Weeks Available and if the plot is empty
            int now = simulation.CurrentWeek;
           
            int then = maturity + now;
            if (simulation.GetNumberOfWeeks() - then > 0)
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
            return plotWeeks[simulation.CurrentWeek];
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
        public string getSoilType()
        {
            return this.soiltype.GetName();
        }
        public string[] getAllCropNamesInPlotWithStartEndDates()
        {
            List<string> list = new List<string>();
            int startweek = -1;
            int endweek = -1;
            if(hasCrop == 0)
            {
                return null;
            }
            for (int i = 0; i< plotWeeks.Count; i++)
            {
                if(i == 0 )
                { 
                    if (!plotWeeks[i].isEmpty)
                    {
                        startweek = i;
                    }
                }
                else
                {
                    if (!plotWeeks[i].isEmpty)
                    {
                        if (plotWeeks[i - 1].isEmpty || plotWeeks[i].getCrop() != plotWeeks[i - 1].getCrop() || i == plotWeeks.Count - 1)
                        {
                            if(startweek == -1)
                            {
                                startweek = i;
                            }
                            else if(!plotWeeks[i - 1].isEmpty)
                            {
                                // The Week holds a crop but is different therefore the week before is the end date and the week is the begin date to another crop
                                if(i == plotWeeks.Count - 1)
                                {
                                    endweek = i;
                                }
                                else
                                {
                                    endweek = i - 1;
                                }
                                list.Add(plotWeeks[i - 1].getCrop().GetCropName());
                                DateTime date = simulation.weekToDate(startweek);
                                string Date = simulation.DateToString(date);
                                list.Add("Start Date: " + Date);
                                date = simulation.weekToDate(endweek);
                                Date = simulation.DateToString(date);
                                list.Add("End Date: " + Date);
                                startweek = i;
                                endweek = -1;
                            }
                        }
                        
                    }
                    else if(plotWeeks[i].isEmpty && startweek != -1)
                    {   //The week holds no crop therefore the before is the end date
                        endweek = i - 1;
                        list.Add(plotWeeks[i - 1].getCrop().GetCropName());
                        DateTime date = simulation.weekToDate(startweek);
                        string Date = simulation.DateToString(date);
                        list.Add("Start Date: " + Date);
                        date = simulation.weekToDate(endweek);
                        Date = simulation.DateToString(date);
                        list.Add("End Date: " + Date);
                        startweek = -1;
                        endweek = -1;
                    }
                }

                
            }
            return list.ToArray();
        }
        public int getNumberOfCrops()
        {
            return hasCrop;
        }
        public int getNumberOfHarvestedCrops()
        {
            return cropsHarvested;
        }
        private void setWeatherForWeek(int week)
        {
            DateTime date = simulation.weekToDate(week);
            plotWeeks[week].weather = simulation.database.GetWeather(simulation.Province, date.Month, date.Year);
        }
        private void calTempertaute(Crop c, int CropWeek, int PlotWeek)
        {
            decimal temperature = plotWeeks[PlotWeek].getTemperture() - c.GetTemperature();
            //if(temperature)
        }
        public void setSimulation(Simulation simulation)
        {
            this.simulation = simulation;
        }
        

    }
}
