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
  
        public int CalTotalYield() { return 0; }
        public Decimal CalTotalProfit() { return 0; }
        public Decimal CalTotalCosts() { return 0; }
       
        public Decimal CalTotalWaterCost() { return 0; }
        public Decimal CalTotalFertilizerCost() { return 0; }
    }
}
