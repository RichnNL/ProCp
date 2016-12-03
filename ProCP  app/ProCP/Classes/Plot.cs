using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCP.Classes
{
    class Plot
    {
        public int PlotId;
       //private SoilTtype sotype;
        private int PlotSize;
        public bool IsEmpty;

      //  public Plot(SoilTypr sotype, int PlotSize) { }
        public void setPlotsize(int sqmeters) { }
        public int getPlotsize() { return 0; }
     //   public bool AddCrop(Crop crop,int position id ) { return false; }
     //   public void setSoilType( SoilType so) { }
        public bool RemoveCrop(int getploposition) { return false; }
        public bool RemoveAllCrop(List<int> getploposition) { return false; }
        public CropData GetCurrentCropData() { return null; }
        public CropData GetCropSummary() { return null; }
        public CropData GetCropataByDate(DateTime d) { return null; }
        //Needs some explanation
        public void DrawSelf() { }
        public void NurishLand() { }
        public void Manageweeks() { }
        public void CalBeginToEnd() { }
        public void CalCurrentDate() { }





    }
}
