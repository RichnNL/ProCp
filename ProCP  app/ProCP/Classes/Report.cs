using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCP.Classes
{
    class Report
    {
        private Simulation simulation;
        public Report(Simulation s) { }
        public void createReport(Simulation s);
        public void SaveReport(Simulation s);

    }
}
