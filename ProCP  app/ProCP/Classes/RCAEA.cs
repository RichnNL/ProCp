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
        public RCAEA(string province,DateTime start, DateTime end)
        {
            simulation = new Simulation("connection.ini", "connection.ini", province,start,end);
            submitChange = false;
            simulationStorage = new SimulationStorage("connection.ini");
            simulation.NotSavedEvent += new Simulation.SimulationNotSaved(notsaved);
            simulationStorage.changedSinceLastSave = true;
        }
        public void generateReport(Simulation sim)
        {
            report = new Report(sim);
            // report.generate();
        }
        public bool loadSimulation(string simulationName)
        {
            string[] details;
            List<Plot> loadedSimulationPlots = simulationStorage.LoadSimulation(simulationName,out details);
            if(details[0] != null && loadedSimulationPlots[0] != null)
            {
                string[] Settings = new string[2];
 
                int comma = details[1].IndexOf(",");
                


                Settings[0] = details[1].Substring(0, comma);
                Settings[1] = details[1].Substring(comma + 1, (details[1].Length - 1) - comma); 
               
                simulation.LoadSimualtion(details[0], details[2], details[3], details[4], Settings, loadedSimulationPlots);
                simulationStorage.changedSinceLastSave = true;
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
                    simulationStorage.changedSinceLastSave = true;
                    return true;
                }
                return false;
            }
            return false;
        }
        public void resetSimulation()
        {
            simulation.ResetSimulation();
            simulationStorage.changedSinceLastSave = true;

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
        private void notsaved()
        {
            simulationStorage.changedSinceLastSave = false;
        }
        
        
    }
}
