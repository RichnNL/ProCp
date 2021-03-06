﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCP.Classes
{
    [Serializable]
    class Statistics
    {
        private Simulation simulation;
        public Statistics(Simulation simulation) {
            this.simulation = simulation;
        }
        private List<CropData> Summary()
        {
            List<CropData> list = new List<Classes.CropData>();
            List<Plot> plots = simulation.getListOfPlots();
            foreach (Plot p in plots)
            {

                List<CropData> temp = p.GetCropSummary();
                if (temp == null)
                {
                    continue;
                }
                else
                {
                    foreach (CropData c in temp)
                    {
                        list.Add(c);
                    }
                }
               
            }
            return list;
        }
        public int CalTotalYield()
        {
            int tyield = 0;
            List<CropData> summary = Summary();
            foreach (CropData c in summary)
            {
                tyield += c.GetYield();
            }
            return tyield;
        }
        public Decimal CalTotalProfit()
        {
            List<CropData> summary = Summary();
            decimal tprofit = 0;
            foreach (CropData c in summary)
            {
                tprofit += c.getProfits();
            }
            return tprofit;

        }
        public Decimal CalTotalCosts()
        {
            List<CropData> summary = Summary();
            decimal tcost = 0;

            if(summary != null)
            {
                foreach (CropData c in summary)
                {
                    tcost += c.GetTotalCost();
                }
            }
           
            return tcost;
        }
       
        public Decimal CalTotalWaterCost()
        {
            List<CropData> summary = Summary();
            decimal twater = 0;
            if (summary != null)
            {
                foreach (CropData c in summary)
                {
                    twater += c.GetWaterCost();
                }
            }
            return twater;
        }
        public Decimal CalTotalFertilizerCost()
        {
            List<CropData> summary = Summary();
            decimal tfertilizer= 0;
            if (summary != null)
            {
                foreach (CropData c in summary)
                {
                    tfertilizer += c.GetFertilizerCost();
                }
            }
            return tfertilizer;
        }
        public string getDataSummary()
        {
            //List<CropData> list = new List<Classes.CropData>();
            //List<Plot> plots = simulation.getListOfPlots();
            //foreach(Plot p in plots)
            //{

            //    List<CropData> temp = new List<CropData>();
            //    if(temp == null){
            //        continue;
            //    }
            //    else temp  = p.GetCropSummary();
            //    foreach(CropData c in temp)
            //    {
            //        list.Add(c);
            //    }
            //}




            string a = "Number of Crops: " + simulation.NumberOfCrops.ToString() + " \nTotal cost: " + CalTotalCosts().ToString() + "\nTotal profit " + CalTotalProfit().ToString() + "\nTotal water costs: " + CalTotalWaterCost().ToString() + "\nTotal fertilizer costs: " + CalTotalFertilizerCost().ToString() + "\nTotal yield: " + CalTotalYield().ToString()  + "\n\nType \t  \tCost \t\tYield \t\tProfit ";
            //loop ....
            List<CropData> summary = Summary();
            if (summary != null)
            {
                foreach (CropData c in Summary())
                {
                    a += "\nName \t \t " + c.GetTotalCost().ToString() + "\t \t" + c.GetYield().ToString() + "\t \t " + c.getProfits().ToString();
                }
            }
            else
            {
                a = "No Data available";
            }

            return a;
        }
        public decimal getTotalCostsByCrop(string crop)
        {
            decimal cost = 0;
            List<CropData> summary = Summary();
            if (summary != null)
            {
                foreach (CropData c in summary)
                {
                    if (c.getCropName() == crop)
                    {
                        cost += c.GetTotalCost();
                    }

                }
            }
            return cost;

        }
        public decimal getTotalWaterCostsByCrop(string crop)
        {
            decimal cost = 0;
            List<CropData> summary = Summary();
            if (summary != null)
            {
                foreach (CropData c in summary)
                {
                    if (c.getCropName() == crop)
                    {
                        cost += c.GetWaterCost();
                    }

                }
            }
            return cost;

        }
        public decimal getTotalFertilizerCostsByCrop(string crop)
        {
            decimal cost = 0;
            List<CropData> summary = Summary();
            if (summary != null)
            {
                foreach (CropData c in summary)
                {
                    if (c.getCropName() == crop)
                    {
                        cost += c.GetFertilizerCost();
                    }

                }
            }
            return cost;

        }
        public decimal getTotalCostsByDate(int Week)
        {
            decimal total = 0;
            List<CropData> data = new List<CropData>();
            List<Plot> plots = simulation.getListOfPlots();
            foreach(Plot p in plots)
            {
                if(p.getNumberOfCrops() != 0)
                {
                    int week = Week;
                    Crop crop = null;
                    while(week >= 0)
                    {
                        if (!p.plotWeeks[week].isEmpty)
                        {
                            if(crop != p.plotWeeks[week].getCrop())
                            {
                                crop = p.plotWeeks[week].getCrop();
                                CropData temp = p.GetCropDataByDate(week);
                                data.Add(temp);
                            }
                        }
                        week--;
                    }
                }
            }
            if(data.Count > 0)
            {
                foreach(CropData cd in data)
                {
                    total += cd.GetTotalCost();
                }
            }
            return total;
        }

    }
}
