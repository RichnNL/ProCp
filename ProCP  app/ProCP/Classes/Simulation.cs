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
        public DateTime EndDate { get { return endDate; }  set { endDate = value;
            } }

        private DateTime currentDate;
        public DateTime CurrentDate { get { return currentDate; } set { currentDate = value; } }
        private int numberOfPlotRows;
        private int numberofPlotColumns;

        private string province;
        
        public string Province { get { return province; } set
            {
                province = value;
                database.loadAllWeather(this.province);
                updatePlots();
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
                updatePlots();
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
            this.province = Province;
            setInitalDate();
            plots = new List<Plot>();
            database = new Database();
            simulationStorage = new SimulationStorage(SimulationStorageDatabase);
            InitialPlots();

        }

        public void Run() { }
        public void Stop() { }
        public void Restart() {
            currentDate = beginDate;
        }
        public void Seek(int percentage) { }

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
            dateChanged();
        }
        public void SetEndDate(DateTime date)
        {
            if(BeginDate.AddMonths(3) < date)
            {
                date = date.AddMonths(3);
            }
            endDate = date;
            dateChanged();
        }
        public void SetCurrentDate() { }
        private void InitialPlots() {
            
            for(int i = 0; i< numberofPlotColumns; i++)
            {
                string plotId = "pb";
                for(int j = 0; j < numberOfPlotRows; j++)
                {
                    plotId = plotId + i.ToString() + j.ToString();
                    plots.Add(new Plot(plotId, database.getDefaultSoilType()));
                }
            }
        }
        
        public int GetNumberOfWeeks()
        {
            weeks =Convert.ToInt32( (this.BeginDate.Subtract(this.EndDate)).TotalDays / 7);
            return weeks;
        }
        private void setInitalDate()
        {
            DateTime now = DateTime.Today;
            DateTime then = now.AddMonths(4);
            this.beginDate = now;
            this.endDate = then;
        }

        private void updatePlots()
        {
            foreach (Plot p in plots)
            {
                p.CalBeginToEnd();
            }
        }
        private void dateChanged()
        {
            foreach(Plot p in plots)
            {
                p.Manageweeks();
            }
        }
        public int getCurrentWeek()
        {
            int currentWeek = Convert.ToInt32(this.currentDate.Subtract(this.beginDate).TotalDays) / 7;
            if (currentWeek > 0)
            {
                currentWeek = currentWeek - 1;
            }
            return currentWeek;

        }
        public int getSpecificWeek(DateTime date)
        {
            int currentWeek = Convert.ToInt32(date.Subtract(this.beginDate).TotalDays) / 7;
            if (currentWeek > 0)
            {
                currentWeek = currentWeek - 1;
            }
            return currentWeek;
        }
        public Plot getPlot(string position)
        {
            return null;
            //to do;
        }
        public void addCrop(Crop crop,Plot plot)
        {
            // to do
        }
        public void removeCrop(Plot plot)
        {
            // to do
        }
    }
}
