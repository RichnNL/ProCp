using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCP.Classes
{
    class RCAEA
    {
        public Simulation simulation;
        public Report report;
        public Plot selectedPlot;
        public string componentSelected;
        public bool submitChange;
        public SimulationStorage simulationStorage;
        public RCAEA(string province)
        {
            simulation = new Simulation("connection.ini", "connection.ini", province);
            submitChange = false;
            simulationStorage = new SimulationStorage("connection.ini");
        }
        public void generateReport(Simulation sim)
        {
            report = new Report(sim);
            // report.generate();
        }
        public bool loadSimulation(string simulationName)
        {
           Simulation loadedSimulation = simulationStorage.LoadSimulation(simulationName);
            if(loadedSimulation != null)
            {
                newSimulation(loadedSimulation);
                return true;
            }
            else
            {
                return false;
            }
            
            
        }
        public bool SaveSimulation()
        {
            if(SimulationNameIsSet())
            {
               if(simulationStorage.SaveSimulation(simulation))
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        public void newSimulation(Simulation newSimulation)
        {
            simulation = newSimulation;
        }
        public void setSimulationName(string name)
        {
            simulation.SimulationName = name;
        }
        public void setSimulationDescription(string description)
        {
            simulation.SimulationDescription = description;
        }
        public bool SimulationNameIsSet()
        {
            if (!String.IsNullOrEmpty(simulation.SimulationName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }
}
