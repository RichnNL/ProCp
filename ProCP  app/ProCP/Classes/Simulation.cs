using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Timers;

namespace ProCP.Classes
{
    class Simulation
    {
        public delegate void DrawCropHandler(string pictureBox, string CropName, int ImageNumber);
        public event DrawCropHandler OnDraw;
        Timer time;

        public List<Plot> plots;
    
        private DateTime beginDate;
        public DateTime BeginDate { get { return beginDate; } set { beginDate = value; } }
        
        private DateTime endDate;
        public DateTime EndDate { get { return endDate; }  set { endDate = value;
            } }

        private DateTime currentDate;
        public DateTime CurrentDate { get { return currentDate; } set { currentDate = value; } }
        private int totalWeeks;
        private int currentWeek;
        private int timeBetweenWeeks;
        public int CurrentWeek
        {
            get { return currentWeek; }
            set
            {
                currentWeek = value;
                if(currentWeek == totalWeeks)
                {
                    Stop();
                }
                if(numberOfCrops != 0)
                {
                    currentWeekChanged();
                }
                
                ;
            }
        }
        private int numberOfCrops;

        private int numberOfPlotRows;
        private int numberofPlotColumns;

        private string province;
        
        public string Province { get { return province; } set
            {
                if (numberOfCrops == 0) { 
                province = value;
                database.loadAllWeather(this.province);
                updatePlots();
                 }
            }
        }

        public int PlotSize
        {
            get { return PlotSize; }

            set
            {
                if (numberOfCrops == 0)
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
                    ;
                }
            }
        }
      


        public  Database database;
        private SimulationStorage simulationStorage;
        public Statistics statistics;


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
            numberOfCrops = 0;
            timeBetweenWeeks = 2000;
            time = new Timer(timeBetweenWeeks);
            time.Elapsed += new ElapsedEventHandler(tick);
            //for testing
            plots = new List<Plot>();
            PlotSize = 100;
            numberofPlotColumns = 10;
            numberOfPlotRows = 8;
            this.province = Province;
            setInitalDate();
            database = new Database(Province);
            simulationStorage = new SimulationStorage(SimulationStorageDatabase);
            statistics = new Statistics();
            InitialPlots();

        }
        private void tick(object o, ElapsedEventArgs e)
        {
            CurrentWeek++;
        }
        public void Run() {
            
            time.Start();

            
        }
        public void Stop() {
            time.Stop();
                }
        public void Restart() {
            CurrentWeek = 0 ;
        }
        public void Seek(int percentage) {
            // to do change currentweek based on totalweeks
        }

        public void SetFertilizer(string fer)
        {
            if (numberOfCrops == 0)
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
        }
        
        public void Setwatering(string water)
        {
            if (numberOfCrops == 0)
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
        }

      
        public void SetBeginDate(DateTime date)
        {
            if (numberOfCrops == 0)
            {
                if(date < EndDate.Subtract(new TimeSpan(90, 0, 0, 0)))
                {
                    beginDate = date;
                    totalWeeks = GetNumberOfWeeks();
                    dateChanged();
                }

            }
        }
        public void SetEndDate(DateTime date)
        {
            if (numberOfCrops == 0)
            {
                if (BeginDate.AddMonths(3) < date)
                {
                    date = date.AddMonths(3);
                }
                endDate = date;
                totalWeeks = GetNumberOfWeeks();
                dateChanged();
            }
        }
        
        private void InitialPlots() {
            
            for(int i = 0; i< numberofPlotColumns; i++)
            {
                string plotId = "pb";
                for(int j = 0; j < numberOfPlotRows; j++)
                {
                    plotId = plotId + i.ToString() + j.ToString();
                    plots.Add(new Plot(plotId, database.getDefaultSoilType(),totalWeeks));
                    plotId = "pb";
                }
            }
        }
        
        public int GetNumberOfWeeks()
        {
            int weeks =Convert.ToInt32( (this.EndDate.Subtract(this.BeginDate)).TotalDays / 7);
            return weeks - 1;
        }
        private void setInitalDate()
        {
            DateTime now = DateTime.Today;
            DateTime then = now.AddMonths(3);
            this.beginDate = now;
            this.endDate = then;
            this.totalWeeks = GetNumberOfWeeks();
            currentWeek = 0;
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
       
        public Plot getPlot(string position)
        {
            foreach(Plot p in plots)
            {
                if(p.PlotId == position)
                {
                    return p;
                }
            }
            return null;
        }
        public void addCrop(Crop crop,Plot plot)
        {
            if (plot.AddCrop(crop))
            {
                numberOfCrops++;
            }
        }
        public void removeCrop(Plot plot)
        {
            if (plot.RemoveCrop(currentWeek))
            {
                numberOfCrops--;
            }

        }

        public int getWeekNumAtSpecficDate(DateTime date)
        {
            int currentWeek = Convert.ToInt32(date.Subtract(this.beginDate).TotalDays) / 7;
            if (currentWeek > 0)
            {
                currentWeek = currentWeek - 1;
            }
            return currentWeek;
        }

        private void currentWeekChanged()
        {
            foreach(Plot p in plots)
            {
                if (p.plotWeeks[CurrentWeek].imageChange)
                {
                    drawPlot(p);
                }
            }
        }
        private void drawPlot(Plot p)
        {
            if(OnDraw == null)
            {
                return;
            }
            else
            {
                OnDraw(p.PlotId, p.plotWeeks[CurrentWeek].getCrop().GetCropName(), p.plotWeeks[CurrentWeek].imageNumber);
            }
        }
    }
}
