using System;
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
        private decimal PlotWaterCost { get; set; }
        private decimal PlotFertilizerCost { get; set; }


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
            //Set Weather for plot weeks
            for (int i = 0; i < plotWeeks.Count; i++)
            {
                setWeatherForWeek(i);
            }
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
                    alive = true;
                    if (health > 90)
                    {
                        health_details += " is very healthly.";
                    }
                    else if (health < 90 && health > 50)
                    {
                        health_details += " is need more care.";
                    }
                    else if (health < 50 && health > 20)
                    {
                        health_details += " need more care.";
                    }
                    else
                    {
                        health_details += " needs urgent care.";
                    }
                    
                }
                else
                {
                    alive = false;
                    health_details = reasonOfDeath(crop);

                }

                if (crop.GetMaturityLength() == now - beginWeek)
                {
                    cropdata = new CropData(simulation.weekToDate(beginWeek), simulation.weekToDate(beginWeek + crop.GetMaturityLength()), alive, health_details, crop.WaterCosts, crop.FertilizerCosts, crop.WaterCosts + crop.FertilizerCosts + seedCost(crop), crop.GetCropYield(), getCropProfits(crop));
                }
                else
                {
                    cropdata = new CropData(simulation.weekToDate(beginWeek), simulation.weekToDate(beginWeek + crop.GetMaturityLength()), alive, health_details, crop.WaterCosts, crop.FertilizerCosts, crop.WaterCosts + crop.FertilizerCosts + seedCost(crop) );
                }
                return cropdata;
            }
            else
            {
                return null;  
            }

        }
        public List<CropData> GetCropSummary()
        {
            List<CropData> list = new List<CropData>();
            int startweek = -1;
            int endweek = -1;
            if (hasCrop == 0)
            {
                return null;
            }
            for (int i = 0; i < plotWeeks.Count; i++)
            {
                if (i == 0)
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
                            if (startweek == -1)
                            {
                                startweek = i;
                            }
                            else if (!plotWeeks[i - 1].isEmpty)
                            {
                                // The Week holds a crop but is different therefore the week before is the end date and the week is the begin date to another crop
                                if (i == plotWeeks.Count - 1)
                                {
                                    endweek = i;
                                }
                                else
                                {
                                    endweek = i - 1;
                                }
                                CropData data = GetCropDataByDate(endweek);
                                list.Add(data);
                                startweek = i;
                                endweek = -1;
                            }
                        }

                    }
                    else if (plotWeeks[i].isEmpty && startweek != -1)
                    {   //The week holds no crop therefore the before is the end date
                        endweek = i - 1;
                        CropData data = GetCropDataByDate(endweek);
                        
                        list.Add(data);
                        startweek = -1;
                        endweek = -1;
                    }
                }


            }
            return list;
        }


    
        public CropData GetCropDataByDate(DateTime d)
        {
            CropData cropdata;
            int now = simulation.getWeekNumAtSpecficDate(d);
            Crop crop;
            int beginWeek;
            if (isNotEmptyAtSpecificWeek(now, out crop, out beginWeek))
            {
                bool alive;
                int health = plotWeeks[now].getCrop().weeks[now - beginWeek].Health;
                string health_details = "Crop " + crop.GetCropName();
                if (health > 1)
                {
                    alive = true;
                    if (health > 90)
                    {
                        health_details += " is very healthly.";
                    }
                    else if (health < 90 && health > 50)
                    {
                        health_details += " is need more care.";
                    }
                    else if (health < 50 && health > 20)
                    {
                        health_details += " need more care.";
                    }
                    else
                    {
                        health_details += " needs urgent care.";
                    }
                }
                else
                {
                    alive = false;
                    health_details = reasonOfDeath(crop);
                }

                if (crop.GetMaturityLength() == now - beginWeek)
                {
                    cropdata = new CropData(simulation.weekToDate(beginWeek), simulation.weekToDate(beginWeek + crop.GetMaturityLength()), alive, health_details, crop.WaterCosts, crop.FertilizerCosts, crop.WaterCosts + crop.FertilizerCosts + seedCost(crop), crop.GetCropYield(), getCropProfits(crop));
                }
                else
                {
                    cropdata = new CropData(simulation.weekToDate(beginWeek), simulation.weekToDate(beginWeek + crop.GetMaturityLength()), alive, health_details, crop.WaterCosts, crop.FertilizerCosts, crop.WaterCosts + crop.FertilizerCosts + seedCost(crop));
                }
                return cropdata;
            }
            else
            {
                return null;
            }
        }
        public CropData GetCropDataByDate(int weekNumber)
        {
            CropData cropdata;
            int now = weekNumber ;
            Crop crop;
            int beginWeek;
            if (isNotEmptyAtSpecificWeek(now, out crop, out beginWeek))
            {
                bool alive;
                int health = plotWeeks[now].getCrop().weeks[now - beginWeek].Health;
                string health_details = "Crop " + crop.GetCropName();
                if (health > 1)
                {
                    alive = true;
                    if (health > 90)
                    {
                        health_details += " is very healthly.";
                    }
                    else if (health < 90 && health > 50)
                    {
                        health_details += " is need more care.";
                    }
                    else if (health < 50 && health > 20)
                    {
                        health_details += " need more care.";
                    }
                    else
                    {
                        health_details += " needs urgent care.";
                    }
                }
                else
                {
                    alive = false;
                    health_details = reasonOfDeath(crop);
                }
          
                if(crop.GetMaturityLength() == now - beginWeek)
                {
                    cropdata = new CropData(simulation.weekToDate(beginWeek), simulation.weekToDate(beginWeek + crop.GetMaturityLength()), alive, health_details, crop.WaterCosts, crop.FertilizerCosts, crop.WaterCosts + crop.FertilizerCosts + seedCost(crop),crop.GetCropYield(), getCropProfits(crop));
                }
                else
                {
                    cropdata = new CropData(simulation.weekToDate(beginWeek), simulation.weekToDate(beginWeek + crop.GetMaturityLength()), alive, health_details, crop.WaterCosts, crop.FertilizerCosts, crop.WaterCosts + crop.FertilizerCosts + seedCost(crop));
                }
                
                return cropdata;
            }
            else
            {
                return null;
            }
        }

        private void NurishLand(int week, Crop crop)
        {
            CalculateWeatherFactors(week);

           
            
            decimal AmountOfWaterToAdd = 0;
           

            if(simulation.Watering == "Minimal")
            {
                AmountOfWaterToAdd = crop.GetWaterMinimum() + (crop.GetWaterMinimum() / 100);
            }
            else if (simulation.Watering == "Sufficent")
            {
                AmountOfWaterToAdd = ((crop.GetWaterMaximum() + crop.GetWaterMinimum()) / 2) - (crop.GetWaterMinimum() / 100);
            }
            else if (simulation.Watering == "Abundant")
            {
                AmountOfWaterToAdd = crop.GetWaterMaximum() - ((crop.GetWaterMaximum() + crop.GetWaterMinimum()) / 10);
            }

            //Give the Plot Enough Water
            if (plotWeeks[week].Water < AmountOfWaterToAdd)
            {
                AmountOfWaterToAdd = AmountOfWaterToAdd - plotWeeks[week].Water;
                AddWaterToSoil(week, AmountOfWaterToAdd);
                PlotWaterCost += addWaterCost(AmountOfWaterToAdd);
                crop.WaterCosts += addWaterCost(AmountOfWaterToAdd);
            }

            //Nutrition Factors 
            decimal AmountOfFertilizerToAdd = 0;

            if (simulation.Fertilizer == "Minimal")
            {
                AmountOfFertilizerToAdd = crop.GetNeededNutrition() ;
            }
            else if (simulation.Fertilizer == "Sufficent")
            {
                AmountOfFertilizerToAdd = (crop.GetNeededNutrition() * Convert.ToDecimal(1.25));
            }
            else if (simulation.Fertilizer == "Abundant")
            {
                AmountOfFertilizerToAdd = (crop.GetNeededNutrition() * Convert.ToDecimal(1.5));
            }

            //Give the Plot Enough Nutrition
            if (plotWeeks[week].SoilNutrition < AmountOfFertilizerToAdd)
            {
                AmountOfFertilizerToAdd = AmountOfFertilizerToAdd - plotWeeks[week].SoilNutrition;
                AddFertilizerToSoil(week, (AmountOfFertilizerToAdd));
                PlotFertilizerCost += addFertilizerCost(AmountOfFertilizerToAdd);
                crop.FertilizerCosts += addFertilizerCost(AmountOfFertilizerToAdd);
            }
        }
    

        private void CalculateWeatherFactors(int week)
        {
            //Rain 
            AddWaterToSoil(week, plotWeeks[week].weather.GetRain());
            //Sun
            decimal temp = plotWeeks[week].weather.GetTemp();
            if (temp > 9 && temp < 15)
            {
                AddWaterToSoil(week, -5);
            }
            else if(temp > 14 && temp < 20)
            {
                AddWaterToSoil(week, -10);
            }
        }
    
       
        private void AddWaterToSoil(int week, decimal waterAmount)
        {
            
            //Get the Water from the week before
            if (week != 0)
            {
                plotWeeks[week].Water = plotWeeks[week - 1].Water;
            }
            // Calculate the Water Lose Rate
            plotWeeks[week].Water -= soiltype.GetWaterLooseRate();
            

            //Add Water Amount
            plotWeeks[week].Water += waterAmount;
            if (plotWeeks[week].Water > soiltype.GetMaximumWater())
            {
                plotWeeks[week].Water = soiltype.GetMaximumWater();

            }


        }
        private void AddFertilizerToSoil(int week, decimal fertilizerAmount)
        {
            //Need to add costs 
            //Get the Nutrition from the week before
            if (week != 0)
            {
                plotWeeks[week].SoilNutrition = plotWeeks[week - 1].SoilNutrition;
            }
            // Calculate the Nutrition Lose Rate
            plotWeeks[week].SoilNutrition -= soiltype.GetNutritionLooseRate();


            //Add Water Amount
            plotWeeks[week].SoilNutrition += fertilizerAmount;
            if (plotWeeks[week].SoilNutrition > soiltype.GetMaximumNutrition())
            {
                plotWeeks[week].SoilNutrition = soiltype.GetMaximumNutrition();

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
            PlotWaterCost = 0;
            PlotFertilizerCost = 0;
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
                        CalculateWeatherFactors(startWeek);
                        
                    }
                    else
                    {
                        startWeek++;
                    }
                }
               

            }
        }
        private void CalCurrentDate(int week,int CropMaturity,Crop crop) {
            if(isNotEmptyAtSpecificWeek(week))
            {
                // run Caluclations for that week

                // First Cultivate Lands
                NurishLand(week, crop);
                if(CropMaturity == 0)
                {
                    //Crop is being intialized
                    crop.FertilizerCosts = 0;
                    crop.WaterCosts = 0;
                    plotWeeks[week].imageNumber = 0;
                    plotWeeks[week].imageChange = true;
                }
                else if( CropMaturity == crop.GetMaturityLength() && crop.weeks[CropMaturity].Health > 0)
                {
                    //Crop Finished 
                    if(plotWeeks[week +1] != null && plotWeeks[week + 1].isEmpty)
                    {
                        plotWeeks[week].imageChange = true;
                        plotWeeks[week].imageNumber = -1;
                    }
                    
                    cropsHarvested++;
                }
                else if(crop.weeks[CropMaturity].Health == 0 && crop.weeks[CropMaturity - 1].Health != 0)
                {
                    //Crop Dead
                    plotWeeks[week].imageChange = true;
                    plotWeeks[week].imageNumber = 5;
                }
                else
                {
                    //Run Calculations for Crop
                    CalculateCropWater(week, CropMaturity, crop);
                    CalculateCropNutrition(week, CropMaturity, crop);
                    CalculateCropTemperature(week, CropMaturity, crop);
                    CalculateCropGrowth(week, CropMaturity, crop);
                }
                
            }
            
        }
        private void CalculateCropNutrition(int PlotWeek, int CropWeek, Crop crop)
        {
            decimal fertilizer = plotWeeks[PlotWeek].SoilNutrition - crop.GetNeededNutrition();
            if (plotWeeks[PlotWeek].SoilNutrition < crop.GetNeededNutrition())
            {
                // Not enough Nutrition
                crop.setCropHealth(-15, CropWeek);
            }

            // Crop Absorb Nutrients
            AddFertilizerToSoil(PlotWeek, -(crop.GetNeededNutrition()));
        }
        private void CalculateCropTemperature(int PlotWeek, int CropWeek, Crop crop)
        {
            decimal temp = plotWeeks[PlotWeek].weather.GetTemp();
            decimal tempDifference = temp - crop.GetTemperature();
            if(tempDifference < -4)
            {
                // Too cold 
                crop.setCropHealth(-20, CropWeek);
            }
            else if(tempDifference > 5)
            {
                //Too Hot
                crop.setCropHealth(-5, CropWeek);
            }
        }
      
        private void CalculateCropGrowth(int PlotWeek, int CropWeek, Crop crop)
        {
            int maturity = crop.GetMaturityLength() /4;
            int stage1, stage2, stage3;
            stage1 = maturity;
            stage2 = maturity * 2;
            stage3 = maturity * 3;
            if(CropWeek == stage1)
            {
                plotWeeks[PlotWeek].imageChange = true;
                plotWeeks[PlotWeek].imageNumber = 1;
            }
            else if (CropWeek == stage2)
            {
                plotWeeks[PlotWeek].imageChange = true;
                plotWeeks[PlotWeek].imageNumber = 2;
            }
            else if (CropWeek == stage3)
            {
                plotWeeks[PlotWeek].imageChange = true;
                plotWeeks[PlotWeek].imageNumber = 3;
            }
        }
        private void CalculateCropWater(int PlotWeek, int CropWeek, Crop crop)
        {
            decimal water = plotWeeks[PlotWeek].Water - crop.GetThirst();
            if (plotWeeks[PlotWeek].Water < crop.GetWaterMinimum())
            {
                // Not enough Water
                crop.setCropHealth((int)water, CropWeek);
            }
            else if (plotWeeks[PlotWeek].Water > crop.GetWaterMaximum())
            {
                //Too much water
                crop.setCropHealth(-5, CropWeek);
            }
            else if (water > 0 && water <= 10)
            {
                // Right Amount of Water
                crop.setCropHealth(2, CropWeek);
            }

                // Crop Absorb Water
                AddWaterToSoil(PlotWeek, -(crop.GetThirst()));
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

        private decimal seedCost(Crop crop)
        {
            decimal cost = crop.GetBuyPrice() * simulation.PlotSize;
            return cost;
        }
        private decimal addWaterCost(decimal Amount)
        {
            //TODO ZISIS!!!
            return 1;
        }
        private decimal addFertilizerCost(decimal Amount)
        {
            //TODO ZISIS
            return 1;
        }
        private decimal getCropProfits(Crop crop)
        {
            return 1;
        }
        
        private string reasonOfDeath(Crop crop)
        {
            int weekofdeath = 0;
            int weekThatHoldsCrop = 0;
            for(int i = 0; i < plotWeeks.Count; i++)
            {
                if(!plotWeeks[i].isEmpty && plotWeeks[i].getCrop() == crop)
                {
                    weekThatHoldsCrop = i;
                    break;
                }
            }
            for(int i = weekofdeath; i < crop.GetMaturityLength(); i++)
            {
                if(crop.weeks[i].Health < 4)
                {
                    weekofdeath = i -1;
                    break;
                }
                else
                {
                    weekThatHoldsCrop++;
                }
                


            }
            if(plotWeeks[weekThatHoldsCrop].Water < crop.GetThirst())
            {
                return crop.GetCropName() + " Died from lack of Water";
            }
            else if(plotWeeks[weekThatHoldsCrop].SoilNutrition < crop.GetNeededNutrition())
            {
                return crop.GetCropName() + " Died from lack of Nutrients";
            }
            decimal temp = plotWeeks[weekThatHoldsCrop].weather.GetTemp();
            decimal tempDifference = temp - crop.GetTemperature();
            if (tempDifference < -4)
            {
                // Too cold 
              return crop.GetCropName() + " died because it is too cold";
            }
            else if (tempDifference > 5)
            {
                //Too Hot
               return crop.GetCropName() + " Died because it is too warm";
            }
            else
            {
                return "Unknown Reasons";
            }


        }

        
    }
}
