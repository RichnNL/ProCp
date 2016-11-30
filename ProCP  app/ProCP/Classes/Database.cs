using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProCP.Classes
{
    class Database
    {
       // private MySQLConnection connection;
        private string connectionInfo;
        private List<Crop> Crops;
        private List<SoilType> SoilTypes;
       // private List<Images> Images;
       // private List<Price> Prices;
      // private List<Weather> Weathers;
      // public Database(MYSQLConnection,String connectionInfo){}
        public bool connectToDataBase() { return false; }
        public void LoadSellPRices();
        public void LoadWeather();
        public void LoadImage();
        public Decimal GetBuyPrice(string CropName);
        public Decimal GetSellPrice(string CropName);
        public List<Crop> GetAllCrops() { return null; }
        public Crop GetCrop(string name) { return null; }
       // public Weather GetWeather(string province,int season) { return null; }
       //public Images GetImage(string Cropname,int ImageNumber) { return null; }
       // public SoilType  GetDefaultSoiType(){return null;}
        





    }
}
