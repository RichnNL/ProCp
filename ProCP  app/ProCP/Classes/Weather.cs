using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCP.Classes
{
    [Serializable]
    class Weather
    {
        private decimal rainamount;
        private decimal Temperature;
        private string Province;
        private int year;
        private int Month {
            set { if (value > 12) { Month = 12; }else if (value < 1) { Month = 1; } }
            get { return Month; } }

        public Weather(string province, int month, int year,decimal rainamount, decimal temp) {
            this.Province = province;
            this.Month = month;
            this.rainamount = rainamount;
            this.Temperature = temp;
            this.year = year;

        }
        public decimal GetRain() { return rainamount; }
        public decimal GetTemp() { return Temperature; }
        public string GetProvince() { return Province; }
        public int getMonth() { return Month; }
        public int getYear() { return year; }
    }
}
