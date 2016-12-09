using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Windows.Forms;

namespace ProCP.Classes
{
    class Database
    {
        public MySqlConnection connection;
        private List<Crop> Crops;
        private List<SoilType> SoilTypes;
        private List<Weather> weathers;
        private List<Price> prices;

        public Database(string ConnectionInfo)
        {
            string connect = connectToDataBase(ConnectionInfo);
            this.connection = new MySqlConnection(connect);

            Crops = GetAllCrops();
            weathers = loadWeather();
            LoadSellPrices();

        
        }
        private string connectToDataBase(string connection)
        {
            string cnx = null;
            try
            {
                using (StreamReader sr = new StreamReader("connection.ini"))
                {
                    cnx = sr.ReadToEnd();

                }
            }
            catch (IOException)
            {
                MessageBox.Show("Connection info could not be read");
            }

            return cnx;
        }
       
        public void LoadSellPrices() {
            List<Price> temp = new List<Price>();

            String sql = "SELECT * FROM weather";
            MySqlCommand command = new MySqlCommand(sql, connection);

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                
                string cropName;
                decimal sellPrice;
                decimal buyPrice;

                while (reader.Read())
                {
                    cropName = Convert.ToString(reader["Type"]);
                    sellPrice = Convert.ToDecimal(reader["Water_Retention"]);
                    buyPrice = Convert.ToDecimal(reader["Nutrients"]);
                    //Zisis fix reader properties
                    temp.Add(new Price(cropName, sellPrice, buyPrice));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            prices = temp;


        }
        public void LoadWeather() { }
        public void LoadImage() { }
        public Decimal GetBuyPrice(string CropName) { return 0; }
        public Decimal GetSellPrice(string CropName) { return 0; }
        
        public List<Crop> GetAllCrops() 
        {
            String sql = "SELECT * FROM crop_info";
            MySqlCommand command = new MySqlCommand(sql, connection);

            List<Crop> temp = new List<Crop>();

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                string cropName;
                int maturity;
                decimal waterMin;
                decimal waterMax;
                decimal thirst;
                decimal neededNutrition;
                decimal nutritionRate;
                decimal temperature;
                string season;
                int yield;
                

                while (reader.Read())
                {
                    cropName = Convert.ToString(reader["Name"]);
                    maturity = Convert.ToInt32(reader["Maturity"]);
                    waterMin = Convert.ToDecimal(reader["Water_Min"]);
                    waterMax = Convert.ToDecimal(reader["Water_Max"]);
                    thirst = Convert.ToDecimal(reader["Thirst"]);
                    neededNutrition = Convert.ToDecimal(reader["Nutrition_Need"]);
                    nutritionRate = Convert.ToDecimal(reader["Nutrition_Rate"]);
                    temperature = Convert.ToDecimal(reader["Temperature"]);
                    season = Convert.ToString(reader["Season"]);
                    yield = Convert.ToInt32(reader["Yield"]);


                    temp.Add(new Crop(cropName, maturity, waterMin, waterMax, thirst, neededNutrition,nutritionRate, temperature, season, yield));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return temp;
            
        }

        public Crop GetCrop(string name) { 
            foreach(Crop c in Crops)
            {
                if(c.GetCropName() == name)
                {
                    return c;
                }
            }
            return null;
        }
        public Weather GetWeather(string province, int month)
        {
            foreach (Weather w in weathers)
            {
                if(w.GetProvince() == province && w.getMonth() == month)
                {
                    return w;
                }
            }
            return null;
        }
        //public Images GetImage(string Cropname,int ImageNumber) { return null; }
        private List<Weather> loadWeather()
        {
            String sql = "SELECT * FROM weather";
            MySqlCommand command = new MySqlCommand(sql, connection);

            List<Weather> temp = new List<Weather>();

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                decimal rainamount;
                decimal Temperature;
                string Province;
                int Month;

                while (reader.Read())
                {
                    Province = Convert.ToString(reader["Province"]);
                    Temperature = Convert.ToDecimal(reader["Temperature"]);
                    rainamount = Convert.ToDecimal(reader["Nutrients"]);
                    Month = Convert.ToInt32(reader["Months"]);
                    // Zisis input right reader properties
                    temp.Add(new Weather(Province,Month,rainamount,Temperature));

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return temp;

        }

        public List<SoilType>  GetAllSoilTypes()
        {
            String sql = "SELECT * FROM soil";
            MySqlCommand command = new MySqlCommand(sql, connection);

            List<SoilType> temp = new List<SoilType>();

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                String name;
                int max_water;
                int nutrients;

                while (reader.Read())
                {
                    name = Convert.ToString(reader["Type"]);
                    max_water = Convert.ToInt32(reader["Water_Retention"]);
                    nutrients = Convert.ToInt32(reader["Nutrients"]);

                    temp.Add(new SoilType(name, max_water, nutrients));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return temp;

            
        }
        
        





    }
}
