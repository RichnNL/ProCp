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

        public Statistics() { }
        public int CalTotalYield() { return 0; }
        public Decimal CalTotalProfit() { return 0; }
        public Decimal CalTotalCosts() { return 0; }
       // public Decimal CalCostByCrop(Crop crop) { return 0; }
        public Decimal CalTotalWaterCost() { return 0; }
        public Decimal CalTotalFertilizerCost() { return 0; }
    }
}
