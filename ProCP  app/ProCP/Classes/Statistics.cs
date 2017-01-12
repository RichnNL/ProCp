using System;
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

                List<CropData> temp = new List<CropData>();
                if (temp == null)
                {
                    continue;
                }
                else temp = p.GetCropSummary();
                foreach (CropData c in temp)
                {
                    list.Add(c);
                }
            }
            return list;
        }
        public int CalTotalYield()
        {
            int tyield = 0;
            foreach (CropData c in Summary())
            {
                tyield += c.GetYield();
            }
            return tyield;
        }
        public Decimal CalTotalProfit()
        {
            decimal tprofit = 0;
            foreach (CropData c in Summary())
            {
                tprofit += c.getProfits();
            }
            return tprofit;

        }
        public Decimal CalTotalCosts()
        {
            decimal tcost = 0;


            foreach (CropData c in Summary())
            {
                tcost += c.GetTotalCost();
            }
            return tcost;
        }
       
        public Decimal CalTotalWaterCost()
        {
            decimal twater = 0;
            foreach (CropData c in Summary())
            {
                twater += c.GetWaterCost();
            }
            return twater;
        }
        public Decimal CalTotalFertilizerCost()
        {
            decimal tfertilizer= 0;
            foreach (CropData c in Summary())
            {
                tfertilizer += c.GetFertilizerCost();
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




            string a = "Number of Crops: " + simulation.getNumberOfCrops().ToString() + " \nTotal cost: " + CalTotalCosts().ToString() + "\nTotal profit " + CalTotalProfit().ToString() + "\nTotal water costs: " + CalTotalWaterCost().ToString() + "\nTotal fertilizer costs: " + CalTotalFertilizerCost().ToString() + "\nTotal yield: " + CalTotalYield().ToString()  + "\n\nType \t  \tCost \t\tYield \t\tProfit ";
            //loop ....

            foreach (CropData c in Summary())
            {
                a += "\nName \t \t " + c.GetTotalCost().ToString() + "\t \t" + c.GetYield().ToString() + "\t \t " + c.getProfits().ToString();
            }

            return a;
        }
    }
}
