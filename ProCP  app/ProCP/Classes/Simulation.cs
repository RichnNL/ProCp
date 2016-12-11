using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ProCP.Classes
{
    class Simulation
    {   private List<Plot> plots;
    
        private DateTime beginDate;
        public DateTime BeginDate { get { return beginDate; } set { beginDate = value; } }
        
        private DateTime endDate;
        public DateTime EndDate { get { return endDate; }  set { endDate = value; } }

        private DateTime currentDate;
        public DateTime CurrentDate { get { return currentDate; } set { currentDate = value; } }
        private int numberOfPlotRows;
        private int numberofPlotColumns;

        private string province;
        
        public string Province { get { return province; } set
            {
                province = value;
                database.loadWeather(province);
            }
        }

        private int PlotSize
        {
            get { return PlotSize; }

            set
            {
                if (value < 50)
                {
                    PlotSize = 50;
                }
                else if (value > 250)
                {
                    PlotSize = 250;
                }
            ;}
        }
      


        public static Database database;
        private SimulationStorage simulationStorage;
        public Statistics statistics;




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


        public Simulation(string DataBaseConnection,string SimulationStorageDatabase,string Province) {
            PlotSize = 100;
            numberofPlotColumns = 10;
            numberOfPlotRows = 8;
            plots = new List<Plot>();
            database = new Database(DataBaseConnection,province);
            simulationStorage = new SimulationStorage(SimulationStorageDatabase);
            InitialPlots();

        }

        public void Run() { }
        public void Stop() { }
        public void Restart() { }
        public void Seek(int percentage) { }

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
        private void InitialPlots() {
            
            for(int i = 0; i< numberofPlotColumns; i++)
            {
                string plotId = "pb";
                for(int j = 0; j < numberOfPlotRows; j++)
                {
                    //will finish later
                }
            }
        }
        
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
        private void setInitalDate()
        {
            //Tsank to do
        }
    }
}
