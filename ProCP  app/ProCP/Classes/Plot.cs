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
       //private SoilTtype sotype;
        private int plotSize;
        public int PlotSize { get { return plotSize; } set { plotSize = value; } }
        public bool IsEmpty;
        private List<PlotWeek> plotWeeks;
        


        public Plot()
        {
            plotWeeks = new List<PlotWeek>();
        }
      //  public Plot(SoilTypr sotype, int PlotSize) { }
        public void setPlotsize(int sqmeters) { }
        public int getPlotsize() { return 0; }
        public bool AddCrop(string crop, string pictureboxId)
        {
            if (IsEmpty)
            {
                Crop c = new Crop(crop);
                Manageweeks(c.GetMaturityLength());
                foreach (PlotWeek p in plotWeeks)
                {
                    p.setCrop(c);
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





    }
}
