using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProCP.Classes
{
    class Plot
    {
        public string PlotId;
        private SoilType soiltype;
        private int plotSize;
        public int PlotSize { get { return plotSize; } set { plotSize = value; } }
        public bool IsEmpty;
        private List<PlotWeek> plotWeeks;
        private Database db;


        public Plot(string PlotId,SoilType soiltype, int PlotSize)
        {
            plotWeeks = new List<PlotWeek>();
            setPlotsize(PlotSize);

        }
      //  public Plot(SoilTypr sotype, int PlotSize) { }
        public void setPlotsize(int sqmeters) { }
        public int getPlotsize() { return 0; }
        public bool AddCrop(string crop, string pictureboxId)
        {
            if (IsEmpty)
            {
                
                Crop cr= db.GetCrop(crop);

                Manageweeks(cr.GetMaturityLength());
                foreach (PlotWeek p in plotWeeks)
                {   
                    p.setCrop(cr);
                    
                    
                }
                PlotId = pictureboxId;
                IsEmpty = false;
                return true;
            }
            else

                return false;
        }

     //   public void setSoilType( SoilType so) { }
        public bool RemoveCrop(int getploposition) { return false; }
        public bool RemoveAllCrop(List<int> getploposition) { return false; }
        public CropData GetCurrentCropData() { return null; }
        public CropData GetCropSummary() { return null; }
        public CropData GetCropataByDate(DateTime d) { return null; }
        //Needs some explanation
        public void DrawSelf() { }
        public void NurishLand() { }

        // WRONG WRONG WRONG !
        private void Manageweeks(int nbrWeeks)
        {
            for (int i = 0; i < nbrWeeks; i++)
            {
                plotWeeks.Add(new PlotWeek());
            }
        }
        /// <summary>
        /// Assigning PlotSize of the current plot object
        /// </summary>
        /// <param name="size"></param>
        public void SetPlotSize(int size)
        {
            PlotSize = size;
        }


        public void CalBeginToEnd() { }
        public void CalCurrentDate() { }

        private void setSoilType(SoilType soiltype)
        {
            this.soiltype = soiltype;
            //Will do later need to change first plot week
        }



    }
}
