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

        private int fertilizer;
        public int Fertilizer
        {
            get { return fertilizer; }
            set { fertilizer = value; }
        }

        private int water;
        public int Watering
        {
            get { return water; }
            set { water = value; }
        }


        // public Simulation() { plots = new List<Plot>(); }

        public void Run() { }
        public void Stop() { }
        public void Restart() { }
        public void Seek() { }

        /// <summary>
        /// Seting Fertilizer
        /// </summary>
        /// <param name="fer"></param>
        public void SetFertilizer(string fer)
        {
            switch (fer)
            {
                case "small":
                    {
                        this.Fertilizer = 1000;
                    }
                    break;
                case "medium":
                    {
                        this.Fertilizer = 2000;
                    }
                    break;
                case "large":
                    {
                        this.Fertilizer = 3000;
                    }
                    break;
            }
        }
        /// <summary>
        /// Seting watering
        /// </summary>
        /// <param name="water"></param>
        public void Setwatering(string water)
        {
            switch (water)
            {
                case "small":
                    {
                        this.Watering = 3000;
                    }
                    break;
                case "medium":
                    {
                        this.Watering = 6000;
                    }
                    break;
                case "large":
                    {
                        this.Watering = 9000;
                    }
                    break;
            }
        }

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
