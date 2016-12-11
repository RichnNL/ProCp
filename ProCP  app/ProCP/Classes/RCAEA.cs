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
        public RCAEA(string province)
        {
            simulation = new Simulation("connection.ini", "connection.ini", province);

        }
        public void generateReport(Simulation sim)
        {
            report = new Report(sim);
            // report.generate();
        }
    }
}
