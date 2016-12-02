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

        //Number of selects for the selected start 
        private int weeks;
        public int Weeks
        {
            get { return weeks; }
            set { weeks = value; }
        }


       // public Simulation() { plots = new List<Plot>(); }
       
        public void Run() { }
        public void Stop() { }
        public void Restart() { }
        public void Seek() { }
        public void SetFertilizer(string fer) { }
        public void Setwatering(string water) { }
        public void SetProvince(int sqmeters) { }
        // public int getPlotPosition(int pictureboxid) { return 0;}
        public void SetBeginDate(DateTime date)
        {
            beginDate = date;
        }
        public void SetEndDate(DateTime date)
        {
            endDate = date;
        }
        public void SetCurrentDate() { }
        private void InitialPlots() { }
        
        /// <summary>
        /// Method which takes the start date and end date as a parameter and return the number of weeks between selected dates.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public int GetNumberOfWeeks(DateTime startDate, DateTime endDate)
        {
            weeks =Convert.ToInt32( (startDate.Subtract(endDate)).TotalDays / 7);
            return weeks;
        }





        
        
    }
}
