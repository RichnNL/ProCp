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

        public int Health
        {
            get { return health; }
            set
            {
                if (value > 100) { health = 100; }
                if (value < 0) { health = 0; }
                health = value;
            }
        }
        public Crop_Week() { }
    }
}
