using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCP.Classes
{
    class Weather
    {
        private decimal rainamount;
        private decimal Temperature;
        private string Province;
        private int Month {
            set { if (value > 12) { Month = 12; }else if (value < 1) { Month = 1; } }
            get { return Month; } }

        public Weather(string province, int month, decimal rainamount, decimal temp) {
            this.Province = province;
            this.Month = month;
            this.rainamount = rainamount;
            this.Temperature = temp;
        }
        public decimal GetRain() { return rainamount; }
        public decimal GetTemp() { return Temperature; }
        public string GetProvince() { return Province; }
        public int getMonth() { return Month; }
    }
}
