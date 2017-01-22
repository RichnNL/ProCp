using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCP.Classes
{
    [Serializable]
    class Crop_Week
    {
      
        public int maturity;
        private int health;
<<<<<<< HEAD
        private int currentImage;
        public int CurrentImage
        {
            get { return currentImage; }
            set { if(value > 4) { currentImage = 4; }
                  if(value < 1) { currentImage = 1; }
                    }
        }
=======

>>>>>>> origin/master
        public int Health
        {
            get { return health; }
            set
            {
                if (value > 100) { health = 100; }
                else if (value < 0) { health = 0; }
                else { health = value; }

            }
        }
        public Crop_Week() { }
    }
}
