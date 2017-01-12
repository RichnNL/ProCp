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
  
        public int CalTotalYield() {
            return 0;
        }
        public Decimal CalTotalProfit()
        {
            return 0;
        }
        public Decimal CalTotalCosts()
        {
            return 0;
        }
       
        public Decimal CalTotalWaterCost()
        {
            return 0;
        }
        public Decimal CalTotalFertilizerCost()
        {
            return 0;
        }
        public string getDataSummary()
        {
            List<CropData> list = new List<Classes.CropData>();
            List<Plot> plots = simulation.getListOfPlots();
            foreach(Plot p in plots)
            {
                
                List<CropData> temp = new List<CropData>();
                if(temp != null){
                    continue;
                }
                  temp  = p.GetCropSummary();
                foreach(CropData c in temp)
                {
                    list.Add(c);
                }
            }




            string a = "Number of Crops: "  + " \nTotal: " + "\nCrops \t  \tCost \t\tYield \t\tProfit ";
            //loop ....
            foreach (CropData c in list)
            {
                a += "\nCarrots \t \t " + c.GetTotalCost().ToString() + "\t \t" + c.GetYield().ToString() + "\t \t50";
            }

            return a;
        }
    }
}
