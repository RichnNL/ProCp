using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCP.Classes
{
    class Simulation
    {  // private List<Plot> plots;
        private DateTime beginDate;
        public DateTime BeginDate { get; set; }
        
        private DateTime endDate;
        public DateTime EndDate { get; set; }

        private DateTime currentDate;
        public DateTime CurrentDate { get; set; }

        private string province;
        public DateTime Province { get; set; }

       // public Simulation() { plots = new List<Plot>(); }
       
        public void Run() { }
        public void Stop() { }
        public void Restart() { }
        public void Seek() { }
        public void SetFertilizer(string fer) { }
        public void Setwatering(string water) { }
        public void SetProvince(int sqmeters) { }
     // public int getPlotPosition(int pictureboxid) { return 0;}
        public void SetBeginDate(DateTime date) { }
        public void SetEndDate(DateTime date) { }
        public void SetCurrentDate() { }
        private void InitialPlots() { }






        
        
    }
}
