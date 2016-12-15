using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCP.Classes
{
    class RCAEA
    {
        public static Simulation simulation;
        public Report report;
        public Plot selectedPlot;
        public string componentSelected;
        public bool submitChange;
        public CropData cropdata_now;
        public CropData cropdata_summary;
        public RCAEA(string province)
        {
            simulation = new Simulation("connection.ini", "connection.ini", province);
            submitChange = false;
        }
        public void generateReport(Simulation sim)
        {
            report = new Report(sim);
            // report.generate();
        }
        
    }
}
