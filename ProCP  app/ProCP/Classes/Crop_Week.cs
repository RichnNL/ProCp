using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCP.Classes
{
    class Crop_Week
    {
      
        public int maturity;
        private int health;
        public bool Imagechanged;
        private int currentImage;
        public int CurrentImage
        {
            get { return currentImage; }
            set { if(value > 4) { currentImage = 4; }
                  if(value < 1) { currentImage = 1; }
                    }
        }
        public int Health
        {
            get { return health; }
            set
            {
                if (value > 100) { currentImage = 100; }
                if (value < 0) { currentImage = 0; }
            }
        }
        public Crop_Week() { }
    }
}
