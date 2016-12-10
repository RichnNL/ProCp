using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCP.Classes
{
    class SimulationStorage
    {
        public bool changedSinceLastSave;
        private FileHandler filehandler;
        private string ConnectionInfo;
        public  SimulationStorage(string connection) { }
        public void SaveSimulation(string filename) { }
        public void SaveSimulationAs(string filename) { }
        public Simulation LoadSimulation() { return null; }

    }
}
