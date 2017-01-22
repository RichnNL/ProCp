using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Timers;

namespace ProCP.Classes
{
    [Serializable]
    class Simulation
    {
       
        public delegate void DrawCropHandler(string pictureBox, string CropName, int ImageNumber);
        public delegate void SimulationChangedHandler(string itemChanged, string value);
        public delegate void SimulationNotSaved();
        [field: NonSerialized]
        public event DrawCropHandler OnDraw;
        [field: NonSerialized]
        public event SimulationChangedHandler SimulationChangedEvent;
        [field: NonSerialized]
        public event SimulationNotSaved NotSavedEvent;
        [field: NonSerialized]
        Timer time;
        public string SimulationName { get; set; }
        private volatile bool seeking;
        public string SimulationDescription { get; set; }

        private int totalticks;
        private int totalTicks {
            get { return totalticks; }
            set { totalticks = value; if (SimulationChangedEvent != null)
                {
                    SimulationChangedEvent("Tick", value.ToString());
                }; }
        }
        public List<Plot> plots;
    
        private DateTime beginDate;
        public DateTime BeginDate { get { return beginDate; } set {
                if (value != BeginDate)
                {
                    if (NotSavedEvent != null)
                    {
                        NotSavedEvent();
                    }
                    beginDate = value;
                    
                }

                 } }
        
        private DateTime endDate;
        public DateTime EndDate { get { return endDate; }  set {
                if(value != endDate)
                {
                    if(NotSavedEvent != null)
                    {
                        NotSavedEvent();
                    }
                    endDate = value;
                }
                
            } }


        private volatile int totalWeeks;
        private volatile int currentWeek;
        private int timeBetweenWeeks;
        public int CurrentWeek
        {
            get { return currentWeek; }
            set
            {
                if (value != currentWeek)
                {
                    currentWeek = value;
                    if(currentWeek >= totalWeeks)
                    {
                        currentWeek = totalWeeks - 1;
                    }
                    if (numberOfCrops != 0)
                    {
                        currentWeekChanged();
                    }
                    
                   
                }
                
                
                ;
            }
        }
        private volatile int numberOfCrops;

        private int numberOfPlotRows;
        private int numberofPlotColumns;

        private string province;
        
        public string Province { get { return province; } set
            {
                if (value != province)
                {
                    if (NotSavedEvent != null)
                    {
                        NotSavedEvent();
                    }
                }
                if (numberOfCrops == 0) { 
                province = value;
                database.loadAllWeather(this.province);
                updatePlots();
                 }
            }
        }
      
           
        
                  
                  
                    
        
        public void setPlotSize(string plot,int size)
        {
            foreach(Plot p in plots)
            {
                if(p.PlotId == plot)
                {
                    if(p.Ares != size)
                    {
                        p.Ares = size;
                        if (NotSavedEvent != null)
                        {
                            NotSavedEvent();
                        }
                        p.CalBeginToEnd();
                    }
                    
                }
            }
        }
        public int getPlotSize(string plot)
        {
            foreach (Plot p in plots)
            {
                if (p.PlotId == plot)
                {
                    return p.Ares;
                }
            }
            return 0;
        }
      
        public int NumberOfCrops { get { return numberOfCrops; }
            set {if (numberOfCrops == 0 && value != 0)
                {
                    if(SimulationChangedEvent!= null)
                    {
                        SimulationChangedEvent("NumberOfCrops", "1");
                    }
                }
                numberOfCrops = value;

            if(numberOfCrops == 0)
                {
                    if(SimulationChangedEvent!= null)
                    {
                        SimulationChangedEvent("NumberOfCrops", "0");
                    }
                }
            ; }
        }

        public Database database;
        
        public Statistics statistics;


        private string fertilizer;
        public string Fertilizer
        {
            get { return fertilizer; }
            set {
                if (value != fertilizer)
                {
                    if (NotSavedEvent != null)
                    {
                        NotSavedEvent();
                    }
                }

                fertilizer = value; }
        }

        private string water;
        public string Watering
        {

            get { return water; }
            set {
                if (value != water)
                {
                    if (NotSavedEvent != null)
                    {
                        NotSavedEvent();
                    }
                }
                water = value; }
        }


        public Simulation(string DataBaseConnection,string SimulationStorageDatabase,string Province, DateTime StartDate, DateTime EndDate) {
            numberOfCrops = 0;
            seeking = false;
            totalTicks = 100;
            timeBetweenWeeks = 1000;
            time = new Timer();
            time.Interval = timeBetweenWeeks;
            time.Elapsed += new ElapsedEventHandler(tick);
            Fertilizer = "None";
            Watering = "None";
            plots = new List<Plot>();
            
            numberofPlotColumns = 10;
            numberOfPlotRows = 8;
            this.province = Province;
            
            this.beginDate = StartDate;
            this.endDate = EndDate;
            this.totalWeeks = GetNumberOfWeeks();
            currentWeek = 0;
            database = new Database(Province);
            statistics = new Statistics(this);
            InitialPlots();

        }
        private void tick(object o, ElapsedEventArgs e)
        {
           CurrentWeek = CurrentWeek + 1;
            if(currentWeek == totalWeeks - 1)
            {
                time.Stop();
         
            }
           
        }
        public void Run() {
            if(currentWeek != totalWeeks - 1)
            {
                time.Start();
            }
            
        }
        public void Stop() {
            time.Stop();
                }
        public void Restart() {
            if(SimulationChangedEvent != null)
            {
                SimulationChangedEvent("SetToBeginning", "");
            }
            CurrentWeek = 0 ;
        }
        public void Seek(int percentage)
        {
            decimal percent = Convert.ToDecimal(percentage);
            decimal total = Convert.ToDecimal(totalWeeks);
            decimal totalT = Convert.ToDecimal(totalticks);
            decimal current = (total * percent) / totalT;
            seeking = true;
            CurrentWeek = Convert.ToInt32(Math.Ceiling(current));
            seeking = false;
        }

        public void SetFertilizer(string fer)
        {
                if (fer != Fertilizer)
                {
                    if (NotSavedEvent != null)
                    {
                        NotSavedEvent();
                    }

                this.Fertilizer = fer;
                if(numberOfCrops != 0)
                {
                    foreach(Plot p in plots)
                    {
                        p.CalBeginToEnd();
                    }
                    foreach (Plot p in plots)
                    {
                        if (p.plotWeeks[CurrentWeek].imageChange)
                        {
                            drawPlot(p);
                        }
                    }

                }
            }
               
         }
        
        
        public void Setwatering(string water)
        {
           
              
                if (water!= this.Watering)
                {
                    if (NotSavedEvent != null)
                    {
                        NotSavedEvent();
                    }
                }
                this.Watering = water;
                if (numberOfCrops != 0)
                {
                    foreach (Plot p in plots)
                    {
                        p.CalBeginToEnd();
                    }
                    foreach (Plot p in plots)
                    {
                        if (p.plotWeeks[CurrentWeek].imageChange)
                        {
                            drawPlot(p);
                        }
                    }

                }
            
        }

      
        public void SetBeginDate(DateTime date)
        {
            if (numberOfCrops == 0)
            {
                if (date != beginDate)
                {
                    if (NotSavedEvent != null)
                    {
                        NotSavedEvent();
                    }
                    if (date < EndDate.Subtract(new TimeSpan(90, 0, 0, 0)))
                    {
                        beginDate = date;
                        totalWeeks = GetNumberOfWeeks();
                        dateChanged();
                    }
                }
                

            }
        }
        public void SetEndDate(DateTime date)
        {
            if (numberOfCrops == 0)
            {
                if (date != endDate)
                {
                    if (NotSavedEvent != null)
                    {
                        NotSavedEvent();
                    }
                }
                if (BeginDate.AddMonths(3) < date)
                {
                    date = date.AddMonths(3);
                }
                endDate = date;
                totalWeeks = GetNumberOfWeeks();
                dateChanged();
            }
        }
        
        private void InitialPlots()
        {
            plots.Clear();
            for(int i = 0; i< numberofPlotColumns; i++)
            {
                string plotId = "pb";
                for(int j = 0; j < numberOfPlotRows; j++)
                {
                    plotId = plotId + i.ToString() + j.ToString();
                    plots.Add(new Plot(plotId, database.getDefaultSoilType(),totalWeeks,this));
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
            if(SimulationChangedEvent != null)
            {
                SimulationChangedEvent("BeginDate", DateToString(beginDate));
                SimulationChangedEvent("EndnDate", DateToString(endDate));
            }
          
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
                NumberOfCrops++;
                drawPlot(plot);
                if (NotSavedEvent != null)
                {
                    NotSavedEvent();
                }
            }
        }
        public void removeCrop(Plot plot)
        {
            if (plot.RemoveCrop(currentWeek))
            {
                drawPlot(plot);
                NumberOfCrops--;
              
                    if (NotSavedEvent != null)
                    {
                        NotSavedEvent();
                    }
                
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

            if(SimulationChangedEvent != null && !seeking)
            {
                decimal total = Convert.ToDecimal(totalWeeks) - 1;
                decimal current = Convert.ToDecimal(currentWeek);
                if (total == current)
                {
                    SimulationChangedEvent("Time", "100");
                }
                else
                {
                    int percent = Convert.ToInt32(Math.Ceiling((current / total) * 100));
                    SimulationChangedEvent("Time", percent.ToString());
                }
                if (currentWeek == totalWeeks -1)
                {
                    SimulationChangedEvent("End", "");
                }
               
                  
                   
                
               
            }
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
                OnDraw(p.PlotId, p.plotWeeks[CurrentWeek].getCropNameInPlot(), p.plotWeeks[CurrentWeek].imageNumber);
            }
        }
        public int totalWeek()
        {
            return totalWeeks;
        }
       
        public DateTime weekToDate(int week)
        {
            DateTime date = this.beginDate;
            date = date.AddDays(week * 7);
            return date;
        }
        public string DateToString(DateTime date)
        {
            return date.ToString("dd/MM/yyyy");
        }
        public List<Plot> getListOfPlots()
        {
            return plots.ToList();
        }
        public void ResetSimulation()
        {
            
            setInitalDate();
            foreach(Plot p in plots)
            {
                p.Ares = 100;
            }
            InitialPlots();
            NumberOfCrops = 0;
            for (int i = 0; i < plots.Count; i++)
            {
                if (plots[i].getNumberOfCrops() != 0)
                {
                    drawPlot(plots[i]);
                }
            }
            if (SimulationChangedEvent != null)
            {
                SimulationChangedEvent("Reset", "");

            }
        }
        public void LoadSimualtion(string name, string province, string begindate, string enddate, string[] settings, List<Plot> PLOTS)
        {
            if (SimulationChangedEvent != null)
            {
                SimulationChangedEvent("Reset", "");

            }
            this.SimulationName = name;
            loadDates(Convert.ToDateTime(begindate), Convert.ToDateTime(enddate));
            loadProvince(province);
            loadFertilizer(settings[0]);
            loadWater(settings[1]);
            LoadPlots(PLOTS);
        }
      
        private void loadDates(DateTime begin, DateTime endDate)
        {
            this.beginDate = begin;
            this.endDate = endDate;
            this.totalWeeks = GetNumberOfWeeks();
            currentWeek = 0;
            if(SimulationChangedEvent != null)
            {
                SimulationChangedEvent("BeginDate", DateToString(begin));
                SimulationChangedEvent("EndDate", DateToString(endDate));
            }
        }
        private void loadProvince(string province)
        {
            this.province = province;
            if (SimulationChangedEvent != null)
            {
                SimulationChangedEvent("Province",province);  
            }
        }
        private void loadFertilizer(string fertilizer)
        {
            SetFertilizer(fertilizer);
            if (SimulationChangedEvent != null)
            {
                SimulationChangedEvent("Fertilizer", fertilizer);
            }
        }
        private void loadWater(string water)
        {
            Setwatering(water);
            if (SimulationChangedEvent != null)
            {
                SimulationChangedEvent("Watering", water);
            }
        }
  
        private void LoadPlots(List<Plot> PLOTS)
        {
            NumberOfCrops = 0;
            this.plots.Clear();
            this.plots = PLOTS.ToList();
            for(int i = 0; i < plots.Count; i++)
            {
                plots[i].setSimulation(this);
                
            }
            for(int i = 0; i <plots.Count; i++)
            {
                if(plots[i].getNumberOfCrops() != 0)
                {
                    drawPlot(plots[i]);
                    NumberOfCrops += plots[i].getNumberOfCrops();
                }
            }
        }
       
     
       
    }
}
